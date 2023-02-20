using CRB.TPM;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core.Quartz;
using CRB.TPM.Utils.Abstracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Dapper.SqlMapper;
using static Quartz.Logging.OperationName;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TaskScheduler.Common
{
    public class BaseTest
    {
        protected JobEntity entity;
        protected readonly string _HttpTaskClass = $"{typeof(HttpTask).FullName}, {typeof(HttpTask).Assembly.GetName().Name}";
        protected readonly string _MyTaskClass = $"{typeof(MyTask).FullName}, {typeof(MyTask).Assembly.GetName().Name}";

        public BaseTest()
        {
            //任务描述
            entity = new JobEntity
            {
                Id = Guid.NewGuid(),
                Name = "模拟Http任务",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Cron = "",
                //触发器
                TriggerType = TriggerType.Simple,
                Code = $"Scheduler_{Guid.NewGuid()}",
                Group = $"Http_{Guid.NewGuid()}",
                //简单触发器时间间隔(秒)
                Interval = 1,
                RepeatCount= 0,
                JobClass = _HttpTaskClass,
                //状态
                Status = JobStatus.Running
            };
        }
    }

    /// <summary>
    /// 表示任务实体
    /// </summary>
    public class JobEntity : EntityBase<Guid>
    {
        /// <summary>
        /// 所属模块
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public JobType JobType { get; set; }

        /// <summary>
        /// 任务唯一键
        /// </summary>
        public string JobKey { get; set; }

        /// <summary>
        /// 任务组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 任务类
        /// </summary>
        public string JobClass { get; set; }

        /// <summary>
        /// 触发类型
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 简单触发器时间间隔(秒)
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 简单触发器重复次数，0表示无限
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public JobStatus Status { get; set; }
    }


    public class TaskCollection : CollectionAbstract<ITaskDescriptor>, ITaskCollection
    {
    }

    /// <summary>
    /// 任务类型
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// 通用
        /// </summary>
        [Description("通用")]
        Normal,
        /// <summary>
        /// Http
        /// </summary>
        [Description("HTTP")]
        Http
    }

    /// <summary>
    /// 触发器类型
    /// </summary>
    public enum TriggerType
    {
        [Description("简单触发器")]
        Simple,
        [Description("CRON触发器")]
        Cron
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum JobStatus
    {
        [Description("停止")]
        Stop,
        [Description("运行")]
        Running,
        [Description("暂停")]
        Pause,
        [Description("已完成")]
        Completed,
        [Description("异常")]
        Exception
    }

    public class FileLogger : ILogger<SchedulerServer>
    {
        static protected string delimiter = new string(new char[] { (char)1 });

        public FileLogger(string categoryName)
        {
            this.Name = categoryName;
        }
        class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        Disposable _DisposableInstance = new Disposable();
        public IDisposable BeginScope<TState>(TState state)
        {
            return _DisposableInstance;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return this.MinLevel <= logLevel;
        }
        public void Reload()
        {
            _Expires = true;
        }

        public string Name { get; private set; }

        public LogLevel MinLevel { get; set; }
        public string FileDiretoryPath { get; set; }
        public string FileNameTemplate { get; set; }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
                return;
            var msg = formatter(state, exception);
            this.Write(logLevel, eventId, msg, exception);
        }
        void Write(LogLevel logLevel, EventId eventId, string message, Exception ex)
        {
            EnsureInitFile();

            //TODO 提高效率 队列写！！！
            var log = String.Concat(DateTime.Now.ToString("HH:mm:ss"), '[', logLevel.ToString(), ']', '[',
                  Thread.CurrentThread.ManagedThreadId.ToString(), ',', eventId.Id.ToString(), ',', eventId.Name, ']',
                  delimiter, message, delimiter, ex?.ToString());
            lock (this)
            {
                this._sw.WriteLine(log);
            }
        }

        bool _Expires = true;
        string _FileName;
        protected StreamWriter _sw;
        void EnsureInitFile()
        {
            if (CheckNeedCreateNewFile())
            {
                lock (this)
                {
                    if (CheckNeedCreateNewFile())
                    {
                        InitFile();
                        _Expires = false;
                    }
                }
            }
        }
        bool CheckNeedCreateNewFile()
        {
            if (_Expires)
            {
                return true;
            }
            //TODO 使用 RollingType判断是否需要创建文件。提高效率！！！
            if (_FileName != DateTime.Now.ToString(this.FileNameTemplate))
            {
                return true;
            }
            return false;
        }
        void InitFile()
        {
            if (!Directory.Exists(this.FileDiretoryPath))
            {
                Directory.CreateDirectory(this.FileDiretoryPath);
            }
            var path = "";
            int i = 0;
            do
            {
                _FileName = DateTime.Now.ToString(this.FileNameTemplate);
                path = Path.Combine(this.FileDiretoryPath, _FileName + "_" + i + ".log");
                i++;
            } while (System.IO.File.Exists(path));
            var oldsw = _sw;
            _sw = new StreamWriter(new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read), Encoding.UTF8);
            _sw.AutoFlush = true;
            if (oldsw != null)
            {
                try
                {
                    _sw.Flush();
                    _sw.Dispose();
                }
                catch
                {
                }
            }
        }
    }

    public class SchedulerConfig : IConfig
    {
        /// <summary>
        /// 开启
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 启用日志
        /// </summary>
        public bool Logger { get; set; } = false;

        /// <summary>
        /// 实例名称
        /// </summary>
        [Required(ErrorMessage = "实例名称不能为空")]
        public string InstanceName { get; set; } = "SchedulerServer";

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; } = "QRTZ_";

        /// <summary>
        /// 序列化方式
        /// </summary>
        public QuartzSerializerType SerializerType { get; set; } = QuartzSerializerType.Json;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public QuartzProvider Provider { get; set; } = QuartzProvider.SqlServer;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [Required(ErrorMessage = "数据库连接字符串不能为空")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; } = "default";

        public NameValueCollection ToProps()
        {
            return new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = InstanceName,
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX,Quartz",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.StdAdoDelegate,Quartz",
                ["quartz.jobStore.tablePrefix"] = TablePrefix,
                ["quartz.jobStore.dataSource"] = DataSource,
                ["quartz.dataSource.default.connectionString"] = ConnectionString,
                ["quartz.dataSource.default.provider"] = Provider.ToDescription(),
                ["quartz.serializer.type"] = SerializerType.ToDescription()
            };
        }
    }


    /// <summary>
    /// 表示调度任务服务
    /// </summary>
    public class SchedulerServer : IQuartzServer
    {
        private IScheduler _scheduler;
        private readonly ITaskLogger _logger;
        private readonly IServiceProvider _container;
        private readonly SchedulerConfig _config;

        public SchedulerServer(ITaskLogger logger, IServiceProvider container, SchedulerConfig schedulerConfig)
        {
            _logger = logger;
            _config = schedulerConfig;
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
                if (_config == null || !_config.Enabled)
                    return;

                //调度器工厂
                var factory = new StdSchedulerFactory(_config.ToProps());
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
                //var configProvider = _container.GetService<IConfigProvider>();
                //if (configProvider == null)
                //    return;

                //var config = _container.GetService<SchedulerConfig>();

                if (_config == null || !_config.Enabled)
                    return;

                if (_scheduler == null)
                    return;

                ////调度器工厂
                //var factory = new StdSchedulerFactory(_config.ToProps());

                ////创建一个调度器
                //_scheduler = await factory.GetScheduler(cancellation);

                //绑定自定义任务工厂
                _scheduler.JobFactory = new JobFactory(_container);
                Console.WriteLine("绑定自定义任务工厂...");

                //添加任务监听器
                AddJobListener();
                Console.WriteLine("添加任务监听器...");

                //添加触发器监听器
                AddTriggerListener();
                Console.WriteLine("添加触发器监听器...");

                //添加调度服务监听器
                AddSchedulerListener();
                Console.WriteLine("添加调度服务监听器...");

                //启动
                await _scheduler.Start(cancellation);
                await _logger.Info("任务调度已经开始");
                Console.WriteLine("任务调度已经开始...");
            }
            catch (Exception ex)
            {
                await _logger.Error(ex.Message);
                Console.WriteLine($"任务调度失败...{ex.Message}");
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

            Console.WriteLine($"添加任务 {jobDetail.Key} 将在: {trigger.StartTimeUtc:r} 运行");

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

        #region ==私有方法==

        /// <summary>
        /// 添加调度服务监听器
        /// </summary>
        private void AddSchedulerListener()
        {
            _scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
        }

        /// <summary>
        /// 添加任务监听器
        /// </summary>
        private void AddJobListener()
        {
            _scheduler.ListenerManager.AddJobListener(new JobListener());
        }

        /// <summary>
        /// 添加触发器监听器
        /// </summary>
        private void AddTriggerListener()
        {
            //注意：这里自定义TriggerListener后，需要自行处理执行
            //_scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
        }


        public async Task<IJobDetail?> GetJobDetail(JobKey jobKey)
        {
            return await _scheduler.GetJobDetail(jobKey);
        }

        //

        #endregion
    }

    public class SchedulerListener : ISchedulerListener
    {
        public SchedulerListener()
        {

        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }

        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            //return _repository.UpdateStatus(jobDetail.Key.Group, jobDetail.Key.Name, JobStatus.Running);
             await Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //当调度删除任务时，如果任务状态不是已停止，则表示任务已完成，需修改对应状态
            //if (!await _repository.HasStop(jobKey.Group, jobKey.Name))
            //    await _repository.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Completed);
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //暂停
            //return _repository.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Pause);
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //恢复
            //return _repository.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Running);
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    public class JobListener : IJobListener
    {
        public string Name => "job1_to_job2";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("作业执行被否决.");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("作业即将执行.");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("作业已经执行.");
            return Task.CompletedTask;
        }
    }

    public class TriggerListener : ITriggerListener
    {
        public string Name => "HttpTask";

        public  Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public  Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }


    [Description("自定义Http请求任务")]
    public class HttpTask : TaskAbstract
    {
        public HttpTask(ITaskLogger logger) : base(logger) { }

        public override async Task Execute(TaskExecutionContext context)
        {
            var cts = new CancellationTokenSource();

            //如果超时5分钟则取消令牌
            cts.CancelAfter(TimeSpan.FromMinutes(5));

            var idData = context.JobExecutionContext.JobDetail.JobDataMap["id"];
            if (idData == null)
            {
                await Logger.Error("任务编号不存在");
                Console.WriteLine("任务编号不存在");
                return;
            }

            try
            {
                await Logger.Info("你应该在这里执行Http任务");
                Console.WriteLine("你应该在这里执行Http任务");
            }
            catch (HttpRequestException ex)
            {
                await Logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    await Logger.Error($"任务取消：{ex.Message}");
                }
                else
                {
                    await Logger.Error($"请求超时：{ex.Message}");
                    Console.WriteLine($"请求超时：{ex.Message}");
                }
            }
            finally
            {
                await Logger.Error($"永远执行！");
                Console.WriteLine($"永远执行！");
            }
        }
    }


    [Description("自定义MyTask任务")]
    public class MyTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"自定义MyTask任务运行 at: {DateTimeOffset.Now}");
            return Task.CompletedTask;
        }
    }

    [Description("自定义ExampleJob任务")]
    public class ExampleJob : IJob, IDisposable
    {
        private readonly ILogger<ExampleJob> logger;

        public ExampleJob(ILogger<ExampleJob> logger)
        {
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("{Job} job executing, triggered by {Trigger}", context.JobDetail.Key, context.Trigger.Key);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public void Dispose()
        {
            logger.LogInformation("Example job disposing");
        }
    }


    [Description("自定义Worker后台任务")]
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}