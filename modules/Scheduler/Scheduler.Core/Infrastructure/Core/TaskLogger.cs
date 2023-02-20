using System;
using System.Threading.Tasks;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.Mod.Logging.Core.Infrastructure;

namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Core;

/// <summary>
/// 任务日志
/// </summary>
public class TaskLogger : ITaskLogger
{
    private readonly IJobLogRepository _repository;
    private readonly IConfigProvider _configProvider;

    public TaskLogger(IJobLogRepository repository, IConfigProvider configProvider)
    {
        _repository = repository;
        _configProvider = configProvider;
    }

    public Guid JobId { get; set; }

    public async Task Info(string msg)
    {
        var config = _configProvider.Get<SchedulerConfig>();
        if (!config.Logger)
            return;

        var entity = new JobLogEntity
        {
            JobId = JobId,
            Type = JobLogType.Info,
            Msg = msg,
            CreatedTime = DateTime.Now
        };

        await _repository.Add(entity);
    }

    public async Task Debug(string msg)
    {
        var config = _configProvider.Get<SchedulerConfig>();
        if (!config.Logger)
            return;

        var entity = new JobLogEntity
        {
            JobId = JobId,
            Type = JobLogType.Debug,
            Msg = msg,
            CreatedTime = DateTime.Now
        };

        await _repository.Add(entity);
    }

    public async Task Error(string msg)
    {
        var config = _configProvider.Get<SchedulerConfig>();
        if (!config.Logger)
            return;

        var entity = new JobLogEntity
        {
            JobId = JobId,
            Type = JobLogType.Error,
            Msg = msg,
            CreatedTime = DateTime.Now
        };

        await _repository.Add(entity);
    }
}
