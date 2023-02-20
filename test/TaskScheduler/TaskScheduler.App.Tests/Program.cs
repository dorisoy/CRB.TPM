using CRB.TPM;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using TaskScheduler.Common;

namespace TaskScheduler.App.Tests;

public class Program
{
    public static async Task Main(string[] args)
    {
        // 测试 (MyTask)
        await new Test().Run();

        /*
        初始化...
        初始化完成...
        开始任务调度...
        创建任务...
        触发器配置...
        配置简单任务，间隔1 秒，无限循环...
        创建触发器...
        添加任务 Http_e8050857-b634-4476-8ebc-062635594a0d.Scheduler_22863a20-c6cf-431f-8c53-9be693b66ab2 将在: Sat, 12 Nov 2022 14:42:24 GMT 运行
        任务调度已经开始...
        等待 20 秒...
        自定义MyTask任务运行 at: 2022/11/12 14:42:25 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:25 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:26 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:27 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:28 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:29 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:30 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:31 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:32 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:33 +08:00
        自定义MyTask任务运行 at: 2022/11/12 14:42:34 +08:00
        */

        // 测试 (HttpTask)
        await new Test().Run2();
        /*
        获取调度器工厂...
        创建一个调度器...
        创建任务...
        配置触发器...
        配置触发器-立即启动...
        配置触发器-简单任务...
        创建触发器...
        添加任务 Http_b132ac6b-e14f-458e-a373-069e088cc35d.Scheduler_667b0444-ba09-4cc6-9dbc-b478b2c1c901 将在: Sat, 12 Nov 2022 08:51:56 GMT 运行
        绑定自定义任务工厂...
        添加任务监听器...
        添加触发器监听器...
        添加调度服务监听器...
        任务调度已经开始...
        添加任务成功
        作业即将执行.
        作业即将执行.
        作业即将执行.
        自定义MyTask任务运行 at: 2022/11/12 16:51:57 +08:00
        自定义MyTask任务运行 at: 2022/11/12 16:51:57 +08:00
        你应该在这里执行Http任务
        永远执行！
        作业已经执行.
        作业已经执行.
        作业已经执行.
        作业即将执行.
        自定义MyTask任务运行 at: 2022/11/12 16:51:57 +08:00
        作业已经执行.
        作业即将执行.
        任务编号不存在
        作业已经执行.
        作业即将执行.
        任务编号不存在
        作业已经执行.
        作业即将执行.
        你应该在这里执行Http任务
         */
    }
}

/// <summary>
/// 测试调度任务
/// </summary>
public class Test : BaseTest
{
    private IServiceProvider _serviceProvider;
    private ServiceCollection services;
    private TaskCollection TaskCollection = new();
    protected readonly IDbContext _context;

    /// <summary>
    /// 测试 (MyTask)
    /// </summary>
    /// <returns></returns>
    public async Task Run()
    {
        Console.WriteLine("初始化...");

        // 从工厂获取IScheduler
        ISchedulerFactory sf = new StdSchedulerFactory();
        IScheduler sched = await sf.GetScheduler();
        Console.WriteLine("初始化完成...");

        Console.WriteLine("开始任务调度...");

        // 创建任务
        IJobDetail job = JobBuilder.Create<MyTask>()
            .WithIdentity(entity.Code, entity.Group)
            .Build();
        Console.WriteLine("创建任务...");


        // 触发器配置
        var triggerBuilder = TriggerBuilder.Create()
            .WithIdentity($"trigger_{entity.Code}", entity.Group)
            .StartAt(entity.BeginDate)
            //立即开始
            .StartNow()
            .WithDescription(entity.Name);
        Console.WriteLine("触发器配置...");

        //简单任务
        if (entity.TriggerType == TriggerType.Simple)
        {
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
        Console.WriteLine($"配置简单任务，间隔{entity.Interval} 秒，无限循环...");


        var trigger = triggerBuilder.Build();
        Console.WriteLine("创建触发器...");

        // 添加任务
        await sched.ScheduleJob(job, trigger);
        Console.WriteLine($"添加任务 {job.Key} 将在: {entity.BeginDate:r} 运行");

        // 启动任务调度
        await sched.Start();
        Console.WriteLine("任务调度已经开始...");

        // 等待 20 秒，以便IJob 完成
        Console.WriteLine("等待 20 秒...");

        //等待10秒以显示作业
        await Task.Delay(TimeSpan.FromSeconds(20));

        // 关闭调度程序
        Console.WriteLine("关闭调度程序...");
        await sched.Shutdown(true);
        Console.WriteLine("关闭调度程序完成...");
    }

    /// <summary>
    /// 测试 (HttpTask)
    /// </summary>
    /// <returns></returns>
    public async Task Run2()
    {
        var assembly = typeof(BaseTest).Assembly;

        services = new ServiceCollection();

        //注入日志
        services.AddLogging();

        //添加任务调度功能
        services.AddTaskScheduler(null);

        //注入监听IScheduler事件
        services.TryAddSingleton<ISchedulerListener, SchedulerListener>();

        //注入IJob
        var jobTypes = assembly.GetTypes().Where(m => typeof(IJob).IsImplementType(m)).ToList();
        if (jobTypes.Any())
        {
            foreach (var type in jobTypes)
            {
                //添加类型的服务
                services.AddTransient(type);
            }
        }

        //任务描述
        var taskDescriptor = new TaskDescriptor
        {
            //模块名称
            ModuleCode = "Scheduler",
            //显示名称
            DisplayName = "HttpTask",
            //类名
            ClassFullName = $"{typeof(HttpTask).FullName}, {typeof(HttpTask).Assembly.GetName().Name}"
        };
        //添加到任务列表
        TaskCollection.Add(taskDescriptor);
        //注入调度模块集合
        services.AddSingleton<ITaskCollection>(TaskCollection);


        //注入IConfig
        var jobTypes3 = assembly.GetTypes().Where(m => typeof(IConfig).IsImplementType(m)).ToList();
        if (jobTypes3.Any())
        {
            foreach (var type in jobTypes3)
            {
                //添加类型的服务
                services.AddSingleton(type);
            }
        }

        //注入IQuartzServer
        var jobTypes4 = assembly.GetTypes().Where(m => typeof(IQuartzServer).IsImplementType(m)).ToList();
        if (jobTypes4.Any())
        {
            foreach (var type in jobTypes4)
            {
                //添加类型的服务
                services.AddSingleton(type);
            }
        }

        //获取Provider
        _serviceProvider = services.BuildServiceProvider();

        //var task2 = _serviceProvider.GetService<MyTask>();
   
        //自定义系统配置
        var schedulerConfig = _serviceProvider.GetService<SchedulerConfig>();
        if (schedulerConfig != null)
        {
            schedulerConfig.Provider = QuartzProvider.SqlServer;
            schedulerConfig.ConnectionString = "initial catalog=TPM_Scheduler;data source=DESKTOP-N3DG9IC;password=racing.1;User id=sa;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
            schedulerConfig.Enabled = true;
            schedulerConfig.InstanceName = "SchedulerServer";
            schedulerConfig.Logger = true;
        }

        services.TryAddSingleton<IQuartzServer, SchedulerServer>();

        //添加作业
        await AddHttpJobTest();

    }


    public async Task AddHttpJobTest()
    {
        var _quartzServer = _serviceProvider.GetService<SchedulerServer>();
        if (_quartzServer != null)
            await _quartzServer.Init();

        //指定HttpTask实现类
        var jobClassType = Type.GetType(_HttpTaskClass);
        if (jobClassType == null)
        {
            Console.WriteLine($"任务类({_HttpTaskClass})不存在");
            return;
        }

        //指定key
        var jobKey = new JobKey(entity.Code, entity.Group);

        if (_quartzServer != null)
        {
            var jobDetail = await _quartzServer.GetJobDetail(jobKey);
            if (jobDetail != null)
                return;
        }

        //创建任务
        //var job = JobBuilder.Create<HttpTask>()
        //    .WithIdentity(jobKey)
        //    .UsingJobData("id", entity.Id.ToString())
        //    .Build();

        var job = JobBuilder.Create(jobClassType)
            .WithIdentity(jobKey)
            .UsingJobData("id", entity.Id.ToString())
            .Build();

        Console.WriteLine("创建任务...");


        //创建触发器
        var triggerBuilder = TriggerBuilder.Create()
            .WithIdentity(entity.Code, entity.Group)
             //.EndAt(entity.EndDate.ToUniversalTime())
             .StartAt(entity.BeginDate)
            .WithDescription(entity.Name);
        Console.WriteLine("配置触发器...");


        //立即启动
        triggerBuilder.StartNow();
        Console.WriteLine("配置触发器-立即启动...");

        //简单任务
        if (entity.TriggerType == TriggerType.Simple)
        {
            Console.WriteLine("配置触发器-简单任务...");

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
        //CRON任务
        else
        {
            Console.WriteLine("配置触发器-CRON任务...");

            //CRON表达式无效
            if (!CronExpression.IsValidExpression(entity.Cron))
                //CRON任务
                triggerBuilder.WithCronSchedule(entity.Cron);
        }

        var trigger = triggerBuilder.Build();
        Console.WriteLine("创建触发器...");

        try
        {
            //添加任务
            if (_quartzServer != null)
                await _quartzServer.AddJob(job, trigger);

            //启动
            if (_quartzServer != null)
                await _quartzServer.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"任务调度添加任务失败：{ex}");
        }

        Console.WriteLine($"添加任务成功");
    }
}