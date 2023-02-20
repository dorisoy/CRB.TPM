using CRB.TPM;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core;
using CRB.TPM.TaskScheduler.Core.Quartz;
using CRB.TPM.Utils.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using TaskScheduler.Common;
using Xunit;
using Xunit.Abstractions;

namespace TaskScheduler.Core.Tests
{
    /// <summary>
    /// Quartz 服务测试
    /// </summary>
    public class QuartzServerTests : BaseTest
    {
        private readonly IQuartzServer _quartzServer;
        private readonly ITaskLogger _logger;
        private IServiceProvider _serviceProvider;

        private TaskCollection TaskCollection = new();
        protected readonly IDbContext _context;
        protected ITestOutputHelper _output;


        public  QuartzServerTests(ITestOutputHelper output)
        {
            _output = output;

            var assembly = this.GetType().Assembly;
            var services = new ServiceCollection();

            //注入ITaskLogger
            services.TryAddSingleton<ITaskLogger, TaskLogger>();

            //注入监听IScheduler事件
            services.TryAddSingleton<ISchedulerListener, SchedulerListener>();

            //注入应用关闭事件
            services.TryAddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

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

            var task1 = _serviceProvider.GetService<HttpTask>();
            var task2 = _serviceProvider.GetService<MyTask>();
            //var logger = _serviceProvider.GetService<TaskLogger>();

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

            //
            _quartzServer = _serviceProvider.GetService<SchedulerServer>();

            var _taskCollection = _serviceProvider.GetService<TaskCollection>();
            
            System.Diagnostics.Debug.Print("");
        }


        /// <summary>
        /// 测试任务列表
        /// </summary>
        [Fact]
        public void TaskCollectionTest()
        {
            var count = TaskCollection.Count > 0;
            Assert.True(count);
        }

        /// <summary>
        /// 测试创建HttpTask实例
        /// </summary>
        [Fact]
        public void CreateInstanceTest()
        {
            HttpTask? jobType = new(_logger);
            Assert.NotNull(jobType);
        }



        /// <summary>
        /// 测试HttpTask实现类
        /// </summary>
        [Fact]
        public void HttpJobClassTest()
        {
            var classs = _HttpTaskClass;
            var classs2 = _MyTaskClass;
            Assert.Equal(classs, _HttpTaskClass);
            Assert.Equal(classs2, _MyTaskClass);
        }


        /// <summary>
        /// 测试添加任务成功
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddHttpJobTest()
        {

            //指定HttpTask实现类
            var jobClassType = Type.GetType(_HttpTaskClass);
            if (jobClassType == null)
                Assert.Fail($"任务类({_HttpTaskClass})不存在");

            //指定key
            var jobKey = new JobKey(entity.Code, entity.Group);

            if (_quartzServer != null)
            {
                var jobDetail = await _quartzServer.GetJobDetail(jobKey);
                if (jobDetail != null)
                    return;
            }

            //创建Job
            var job = JobBuilder.Create(jobClassType)
                .WithIdentity(jobKey)
                .UsingJobData("id", Guid.NewGuid())
                .Build();

            //创建触发器
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(entity.Code, entity.Group)
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
            //CRON任务
            else
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                    Assert.Fail($"CRON表达式无效");

                //CRON任务
                triggerBuilder.WithCronSchedule(entity.Cron);
            }

            var trigger = triggerBuilder.Build();

            try
            {
                var offset = await _quartzServer.AddJob(job, trigger);
            }
            catch (Exception ex)
            {
                Assert.Fail($"任务调度添加任务失败：{ex}");
            }

            _output.WriteLine($"添加任务成功");
        }


        /// <summary>
        /// 测试添加任务成功
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddMyTaskTest()
        {
            //指定HttpTask实现类
            var jobClassType = Type.GetType(_MyTaskClass);
            if (jobClassType == null)
                Assert.Fail($"任务类({_MyTaskClass})不存在");

            //指定key
            var jobKey = new JobKey(entity.Code, entity.Group);

            if (_quartzServer != null)
            {
                var jobDetail = await _quartzServer.GetJobDetail(jobKey);
                if (jobDetail != null)
                    return;
            }

            //创建Job
            var job = JobBuilder.Create<MyTask>()
                .WithIdentity(jobKey)
                .UsingJobData("id", Guid.NewGuid())
                .Build();

            //创建触发器
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(entity.Code, entity.Group)
                .EndAt(entity.EndDate.ToUniversalTime())
                .WithDescription(entity.Name);

            //立即启动
            triggerBuilder.StartNow();

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
            //CRON任务
            else
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                    Assert.Fail($"CRON表达式无效");

                //CRON任务
                triggerBuilder.WithCronSchedule(entity.Cron);
            }

            var trigger = triggerBuilder.Build();

            try
            {
                var offset = await _quartzServer.AddJob(job, trigger);
            }
            catch (Exception ex)
            {
                Assert.Fail($"任务调度添加任务失败：{ex}");
            }

            _output.WriteLine($"添加任务成功");
        }



        /// <summary>
        /// 测试暂停任务
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PauseJobTest()
        {
            //指定key
            var jobKey = new JobKey(entity.Code, entity.Group);
            await _quartzServer.PauseJob(jobKey);
            _output.WriteLine($"暂停任务成功");
        }


        /// <summary>
        /// 测试启动任务
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task StartJobTest()
        {
            await _quartzServer.Start();

            ///等待10秒
            await Task.Delay(20000);

            _output.WriteLine($"启动任务成功");

        }
    }
}