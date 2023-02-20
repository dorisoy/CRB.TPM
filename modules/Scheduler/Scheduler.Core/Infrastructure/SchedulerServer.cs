using CRB.TPM;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Module.Abstractions.Options;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Infrastructure;

/// <summary>
/// 表示一个Quartz计划任务服务实现
/// </summary>
public class SchedulerServer : IQuartzServer
{
    private IScheduler _scheduler;
    private readonly ITaskLogger _logger;
    private readonly IServiceProvider _container;

    public SchedulerServer(ITaskLogger logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Init(CancellationToken cancellation = default)
    {
        try
        {
            var configProvider = _container.GetService<IConfigProvider>();
            if (configProvider == null)
                return;

            var config = configProvider.Get<SchedulerConfig>();
            if (config == null || !config.Enabled)
                return;

            //调度器工厂
            var factory = new StdSchedulerFactory(config.ToProps());
            Console.WriteLine("获取调度器工厂...");

            //创建一个调度器
            _scheduler = await factory.GetScheduler(cancellation);
            Console.WriteLine("创建一个调度器...");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"初始化异常：{ex.Message}");
            return;
        }
    }

    /// <summary>
    /// 启动
    /// </summary>
    public async Task Start(CancellationToken cancellation = default)
    {
        try
        {
            var configProvider = _container.GetService<IConfigProvider>();
            if (configProvider == null)
                return;

            var db = _container.GetRequiredService<IDbContext>();
            
            var config = configProvider.Get<SchedulerConfig>();
            if (config == null || !config.Enabled)
                return;

            if (db != null && string.IsNullOrEmpty(config.ConnectionString))
                config.ConnectionString = db.Adapter.Options.ConnectionString;

            //调度器工厂
            var factory = new StdSchedulerFactory(config.ToProps());

            //创建一个调度器
            _scheduler = await factory.GetScheduler(cancellation);

            //绑定自定义任务工厂
            _scheduler.JobFactory = new JobFactory(_container);

            //添加任务监听器
            AddJobListener();

            //添加触发器监听器
            AddTriggerListener();

            //添加调度服务监听器
            AddSchedulerListener();

            //启动
            await _scheduler.Start(cancellation);

            await _logger.Info("Quartz server started");
        }
        catch (Exception ex)
        {
            await _logger.Error(ex.Message);
        }
    }

    /// <summary>
    /// 停止
    /// </summary>
    public async Task Stop(CancellationToken cancellation = default)
    {
        if (_scheduler == null || _scheduler.IsShutdown)
            return;

        await _scheduler.Shutdown(true, cancellation);

        await _logger.Info("Quartz server stopped");
    }

    /// <summary>
    /// 重启
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Restart(CancellationToken cancellation = default)
    {
        await Stop(cancellation);
        await Start(cancellation);
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    public async Task<DateTimeOffset> AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default)
    {
        if (_scheduler == null || _scheduler.IsShutdown)
            return new DateTimeOffset();

        return await _scheduler.ScheduleJob(jobDetail, trigger, cancellation);
    }

    /// <summary>
    /// 暂停任务
    /// </summary>
    public async Task PauseJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        if (_scheduler == null || _scheduler.IsShutdown)
            return;

        await _scheduler.PauseJob(jobKey, cancellation);
    }

    /// <summary>
    /// 恢复任务
    /// </summary>
    public async Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        if (_scheduler == null || _scheduler.IsShutdown)
            return;

        await _scheduler.ResumeJob(jobKey, cancellation);
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    public async Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        if (_scheduler == null || _scheduler.IsShutdown)
            return;

        await _scheduler.DeleteJob(jobKey, cancellation);
    }

    /// <summary>
    /// 获取作业信息
    /// </summary>
    /// <param name="jobKey"></param>
    /// <returns></returns>
    public async Task<IJobDetail?> GetJobDetail(JobKey jobKey)
    {
        return await _scheduler.GetJobDetail(jobKey);
    }

    #region ==私有方法==

    /// <summary>
    /// 添加调度服务监听器
    /// </summary>
    private void AddSchedulerListener()
    {
        var schedulerListeners = _container.GetServices<ISchedulerListener>().ToList();
        if (schedulerListeners.Any())
        {
            foreach (var listener in schedulerListeners)
            {
                _scheduler.ListenerManager.AddSchedulerListener(listener);
            }
        }
    }

    /// <summary>
    /// 添加任务监听器
    /// </summary>
    private void AddJobListener()
    {
        var jobListeners = _container.GetServices<IJobListener>().ToList();
        if (jobListeners.Any())
        {
            foreach (var listener in jobListeners)
            {
                _scheduler.ListenerManager.AddJobListener(listener);
            }
        }
    }

    /// <summary>
    /// 添加触发器监听器
    /// </summary>
    private void AddTriggerListener()
    {
        var triggerListener = _container.GetServices<ITriggerListener>().ToList();
        if (triggerListener.Any())
        {
            foreach (var listener in triggerListener)
            {
                _scheduler.ListenerManager.AddTriggerListener(listener);
            }
        }
    }

    #endregion
}
