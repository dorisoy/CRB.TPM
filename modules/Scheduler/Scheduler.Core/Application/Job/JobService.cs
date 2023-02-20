using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobHttp.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.Tasks;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.Utils.Map;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job;

public class JobService : IJobService
{
    private readonly IMapper _mapper;
    private readonly IAccount _account;
    private readonly IJobRepository _repository;
    private readonly IJobHttpRepository _jobHttpRepository;
    private readonly IJobLogRepository _logRepository;
    private readonly ILogger _logger;
    private readonly IQuartzServer _quartzServer;
    private readonly SchedulerDbContext _dbContext;
    private readonly string _httpJobClass = $"{typeof(HttpTask).FullName}, {typeof(HttpTask).Assembly.GetName().Name}";

    public JobService(IMapper mapper,
        IAccount account,
        IJobRepository repository,
        ILogger<JobService> logger,
        IQuartzServer quartzServer,
        IJobLogRepository logRepository,
        SchedulerDbContext dbContext,
        IJobHttpRepository jobHttpRepository)
    {
        _mapper = mapper;
        _account = account;
        _repository = repository;
        _logger = logger;
        _quartzServer = quartzServer;
        _logRepository = logRepository;
        _dbContext = dbContext;
        _jobHttpRepository = jobHttpRepository;
    }

    /// <summary>
    /// 查询任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Query(JobQueryDto dto)
    {
        var result = await _repository.Query(dto);
        return ResultModel.Success(result);
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Add(JobAddDto dto)
    {
        var entity = _mapper.Map<JobEntity>(dto);
        entity.JobKey = $"{dto.Group}.{dto.Code}";
        entity.Status = JobStatus.Stop;
        entity.EndDate = entity.EndDate.AddDays(1);
        entity.JobType = JobType.Normal;

        if (await _repository.Exists(entity))
        {
            return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
        }

        //是否立即启动
        if (dto.Start)
        {
            var result = await AddJob(entity);
            if (!result.Successful)
            {
                return result;
            }

            entity.Status = JobStatus.Running;
        }

        return ResultModel.Result(await _repository.Add(entity));
    }

    /// <summary>
    /// 编辑任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Edit(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        var model = _mapper.Map<JobUpdateDto>(entity);
        model.EndDate = entity.EndDate.AddDays(-1);

        return ResultModel.Success(model);
    }

    /// <summary>
    /// 更新任务
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel> Update(JobUpdateDto model)
    {
        var entity = await _repository.Get(model.Id);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        _mapper.Map(model, entity);
        entity.JobKey = $"{model.Group}.{model.Code}";
        entity.EndDate = entity.EndDate.AddDays(1);

        if (await _repository.Exists(entity))
        {
            return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
        }

        //如果任务不是停止或者已完成，先删除在添加
        if (entity.Status != JobStatus.Stop && entity.Status != JobStatus.Completed)
        {
            var jobKey = new JobKey(entity.Name, entity.Group);
            await _quartzServer.DeleteJob(jobKey);

            var result = await AddJob(entity, entity.Status == JobStatus.Running);
            if (!result.Successful)
            {
                return result;
            }
        }

        return ResultModel.Result(await _repository.Update(entity));
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Delete(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.NotExists;
        }

        if (entity.Status != JobStatus.Stop && entity.Status != JobStatus.Completed)
        {
            var jobKey = new JobKey(entity.Code, entity.Group);
            await _quartzServer.DeleteJob(jobKey);
        }

        var result = await _repository.Delete(id);
        return ResultModel.Result(result);
    }

    /// <summary>
    /// 暂停任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Pause(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.Failed("任务不存在");
        }

        if (entity.Status == JobStatus.Stop)
        {
            return ResultModel.Failed("任务已停止，无法暂停");
        }

        if (entity.Status == JobStatus.Completed)
        {
            return ResultModel.Failed("任务已完成，无法暂停");
        }

        if (entity.Status == JobStatus.Pause)
        {
            return ResultModel.Failed("任务已暂停，请刷新页面");
        }

        var jobKey = new JobKey(entity.Code, entity.Group);
        await _quartzServer.PauseJob(jobKey);
        return ResultModel.Success();
    }

    /// <summary>
    /// 重启任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Resume(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        if (entity.Status == JobStatus.Running)
        {
            return ResultModel.Failed("任务已启动，请刷新页面");
        }

        //停止的或者已完成的任务，重启需要重新加入到调度中
        if (entity.Status == JobStatus.Stop || entity.Status == JobStatus.Completed)
        {
            if (entity.EndDate <= DateTime.Now)
            {
                return ResultModel.Failed("任务时效已过期");
            }

            var result = await AddJob(entity, true);
            if (!result.Successful)
            {
                return result;
            }
        }
        else
        {
            var jobKey = new JobKey(entity.Code, entity.Group);
            await _quartzServer.ResumeJob(jobKey);
        }
        return ResultModel.Success();
    }

    /// <summary>
    /// 停止任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> Stop(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        if (entity.Status == JobStatus.Stop)
        {
            return ResultModel.Failed("任务已停止，请刷新页面");
        }
        if (entity.Status == JobStatus.Completed)
        {
            return ResultModel.Failed("任务已完成，无法停止");
        }

        if (await _repository.UpdateStatus(id, JobStatus.Stop))
        {
            var jobKey = new JobKey(entity.Code, entity.Group);
            await _quartzServer.DeleteJob(jobKey);

            return ResultModel.Success();
        }

        return ResultModel.Failed();
    }

    /// <summary>
    /// 任务日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> Log(JobLogQueryDto dto)
    {
        var result = await _logRepository.Query(dto);
        return ResultModel.Success(result);
    }

    /// <summary>
    /// 添加Http作业
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<IResultModel> AddHttpJob(JobHttpAddDto dto)
    {
        var entity = _mapper.Map<JobEntity>(dto);
        entity.JobKey = $"{dto.Group}.{dto.Code}";
        entity.Status = JobStatus.Stop;
        entity.EndDate = entity.EndDate.AddDays(1);
        //固定的任务类
        entity.JobClass = _httpJobClass;
        entity.JobType = JobType.Http;

        if (await _repository.Exists(entity))
        {
            return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
        }

        //是否立即启动
        if (dto.Start)
        {
            var result = await AddJob(entity);
            if (!result.Successful)
            {
                return result;
            }

            entity.Status = JobStatus.Running;
        }

        using var uow = _dbContext.NewUnitOfWork();

        //添加job
        if (await _repository.Add(entity, uow))
        {
            var httpJob = _mapper.Map<JobHttpEntity>(dto);
            httpJob.JobId = entity.Id;
            if (dto.HeaderList != null && dto.HeaderList.Any())
            {
                httpJob.Headers = JsonSerializer.Serialize(dto.HeaderList);
            }

            //添加job的http信息
            if (await _jobHttpRepository.Add(httpJob))
            {
                uow.SaveChanges();
                return ResultModel.Success();
            }
        }

        return ResultModel.Failed();
    }

    /// <summary>
    /// 编辑Http作业
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> EditHttpJob(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        var model = _mapper.Map<JobHttpUpdateDto>(entity);
        model.EndDate = entity.EndDate.AddDays(-1);

        var jobHttp = await _jobHttpRepository.GetByJob(id);
        model.Url = jobHttp.Url;
        model.AuthType = jobHttp.AuthType;
        model.ContentType = jobHttp.ContentType;
        model.Method = jobHttp.Method;
        model.Parameters = jobHttp.Parameters;
        model.Token = jobHttp.Token;
        if (jobHttp.Headers.NotNull())
        {
            model.HeaderList = JsonSerializer.Deserialize<List<KeyValuePair<string, string>>>(jobHttp.Headers);
        }

        return ResultModel.Success(model);
    }

    /// <summary>
    /// 更新Http作业
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IResultModel> UpdateHttpJob(JobHttpUpdateDto model)
    {
        using var uow = _dbContext.NewUnitOfWork();
        var entity = await _repository.Get(model.Id, uow);
        if (entity == null)
        {
            return ResultModel.Failed();
        }

        _mapper.Map(model, entity);
        entity.JobKey = $"{model.Group}.{model.Code}";
        entity.EndDate = entity.EndDate.AddDays(1);

        if (await _repository.Exists(entity))
        {
            return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
        }

        if (await _repository.Update(entity, uow))
        {
            var jobHttp = await _jobHttpRepository.GetByJob(entity.Id);
            jobHttp.Url = model.Url;
            jobHttp.AuthType = model.AuthType;
            jobHttp.ContentType = model.ContentType;
            jobHttp.Headers = "";
            jobHttp.Method = model.Method;
            jobHttp.Parameters = model.Parameters;
            jobHttp.Token = model.Token;
            if (model.HeaderList != null)
            {
                jobHttp.Headers = JsonSerializer.Serialize(model.HeaderList);
            }

            if (await _jobHttpRepository.Update(jobHttp, uow))
            {
                uow.SaveChanges();

                //如果任务不是停止或者已完成，先删除在添加
                if (entity.Status != JobStatus.Stop && entity.Status != JobStatus.Completed)
                {
                    var jobKey = new JobKey(entity.Name, entity.Group);
                    await _quartzServer.DeleteJob(jobKey);
                    await AddJob(entity, entity.Status == JobStatus.Running);
                }

                return ResultModel.Success();
            }
        }

        return ResultModel.Failed();
    }

    /// <summary>
    /// Http作业详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultModel> JobHttpDetails(Guid id)
    {
        var entity = await _jobHttpRepository.GetByJob(id);
        if (entity == null)
            return ResultModel.NotExists;

        return ResultModel.Success(entity);
    }

    /// <summary>
    /// 添加任务并立即启动
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="start">是否立即启动</param>
    /// <returns></returns>
    private async Task<IResultModel> AddJob(JobEntity entity, bool start = false)
    {
        var jobClassType = Type.GetType(entity.JobClass);
        if (jobClassType == null)
        {
            return ResultModel.Failed($"任务类({entity.JobClass})不存在");
        }

        var jobKey = new JobKey(entity.Code, entity.Group);
        var job = JobBuilder.Create(jobClassType).WithIdentity(jobKey)
            .UsingJobData("id", entity.Id.ToString()).Build();

        var triggerBuilder = TriggerBuilder.Create().WithIdentity(entity.Code, entity.Group)
            .EndAt(entity.EndDate.ToUniversalTime())
            .WithDescription(entity.Name);

        //如果开始日期小于等于当前日期则立即启动
        if (entity.BeginDate <= DateTime.Now)
        {
            triggerBuilder.StartNow();
        }
        else
        {
            triggerBuilder.StartAt(entity.BeginDate.ToUniversalTime());
        }

        if (entity.TriggerType == TriggerType.Simple)
        {
            //简单任务
            triggerBuilder.WithSimpleSchedule(builder =>
            {
                builder.WithIntervalInSeconds(entity.Interval);
                if (entity.RepeatCount > 0)
                {
                    builder.WithRepeatCount(entity.RepeatCount - 1);
                }
                else
                {
                    builder.RepeatForever();
                }
            });
        }
        else
        {
            if (!CronExpression.IsValidExpression(entity.Cron))
            {
                return ResultModel.Failed("CRON表达式无效");
            }

            //CRON任务
            triggerBuilder.WithCronSchedule(entity.Cron);
        }

        var trigger = triggerBuilder.Build();
        try
        {
            await _quartzServer.AddJob(job, trigger);

            if (!start)
            {
                //先暂停
                await _quartzServer.PauseJob(jobKey);
            }

            return ResultModel.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError("任务调度添加任务失败{@ex}", ex);
        }

        return ResultModel.Failed();
    }
}
