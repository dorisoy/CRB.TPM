using Quartz;
using Serilog;
using TaskScheduler.Common;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();

                // QuartzOptions 配置项
                services.Configure<QuartzOptions>(options =>
                {
                    options.Scheduling.IgnoreDuplicates = true; 
                    options.Scheduling.OverWriteExistingData = true; 
                });

                // 注册配置
                services.AddQuartz(q =>
                {
                    // 任务ID标识
                    q.SchedulerId = "Scheduler-Core";

                    var loggerFactory = new LoggerFactory()
                        .AddSerilog(Log.Logger);

                     //q.SetLoggerFactory(loggerFactory);

                        // q.SchedulerName = "示例调度程序";

                    // 如果不更改，这是默认配置
                    q.UseMicrosoftDependencyInjectionJobFactory();

                    q.UseSimpleTypeLoader();

                    //持久存储
                    q.UsePersistentStore(opt => 
                    {
                        //使用Json序列化
                        opt.UseJsonSerializer();
                        //使用SqlServer存储
                        opt.UseSqlServer("initial catalog=TPM_Scheduler;data source=DESKTOP-N3DG9IC;password=racing.1;User id=sa;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
                    });

                    //默认线程池
                    q.UseDefaultThreadPool(tp =>
                    {
                        tp.MaxConcurrency = 10;
                    });

                    // 使用单个触发器创建作业的最快方法是使用ScheduleJob
                    q.ScheduleJob<ExampleJob>(trigger => trigger
                        .WithIdentity("Combined Configuration Trigger")
                        .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                        .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                        .WithDescription("my awesome trigger configured for a job with single call")
                    );

                    // 添加作业
                    var jobKey = new JobKey("awesome job", "awesome group");
                    q.AddJob<ExampleJob>(j => j
                        .StoreDurably()
                        .WithIdentity(jobKey)
                        .WithDescription("my awesome job")
                    );

                    //添加触发器
                    q.AddTrigger(t => t
                        .WithIdentity("Simple Trigger")
                        .ForJob(jobKey)
                        .StartNow()
                        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                        .WithDescription("my awesome simple trigger")
                    );

                });

                // 添加主机服务
                services.AddQuartzHostedService(options =>
                {
                    // 当关闭应用程序时，等待作业完成
                    options.WaitForJobsToComplete = true;
                    options.StartDelay = TimeSpan.FromSeconds(10);
                });
            });
}

