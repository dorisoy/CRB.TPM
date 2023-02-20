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

                // QuartzOptions ������
                services.Configure<QuartzOptions>(options =>
                {
                    options.Scheduling.IgnoreDuplicates = true; 
                    options.Scheduling.OverWriteExistingData = true; 
                });

                // ע������
                services.AddQuartz(q =>
                {
                    // ����ID��ʶ
                    q.SchedulerId = "Scheduler-Core";

                    var loggerFactory = new LoggerFactory()
                        .AddSerilog(Log.Logger);

                     //q.SetLoggerFactory(loggerFactory);

                        // q.SchedulerName = "ʾ�����ȳ���";

                    // ��������ģ�����Ĭ������
                    q.UseMicrosoftDependencyInjectionJobFactory();

                    q.UseSimpleTypeLoader();

                    //�־ô洢
                    q.UsePersistentStore(opt => 
                    {
                        //ʹ��Json���л�
                        opt.UseJsonSerializer();
                        //ʹ��SqlServer�洢
                        opt.UseSqlServer("initial catalog=TPM_Scheduler;data source=DESKTOP-N3DG9IC;password=racing.1;User id=sa;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");
                    });

                    //Ĭ���̳߳�
                    q.UseDefaultThreadPool(tp =>
                    {
                        tp.MaxConcurrency = 10;
                    });

                    // ʹ�õ���������������ҵ����췽����ʹ��ScheduleJob
                    q.ScheduleJob<ExampleJob>(trigger => trigger
                        .WithIdentity("Combined Configuration Trigger")
                        .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                        .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                        .WithDescription("my awesome trigger configured for a job with single call")
                    );

                    // �����ҵ
                    var jobKey = new JobKey("awesome job", "awesome group");
                    q.AddJob<ExampleJob>(j => j
                        .StoreDurably()
                        .WithIdentity(jobKey)
                        .WithDescription("my awesome job")
                    );

                    //��Ӵ�����
                    q.AddTrigger(t => t
                        .WithIdentity("Simple Trigger")
                        .ForJob(jobKey)
                        .StartNow()
                        .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                        .WithDescription("my awesome simple trigger")
                    );

                });

                // �����������
                services.AddQuartzHostedService(options =>
                {
                    // ���ر�Ӧ�ó���ʱ���ȴ���ҵ���
                    options.WaitForJobsToComplete = true;
                    options.StartDelay = TimeSpan.FromSeconds(10);
                });
            });
}

