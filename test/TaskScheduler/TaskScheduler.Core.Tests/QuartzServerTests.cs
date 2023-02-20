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
    /// Quartz �������
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

            //ע��ITaskLogger
            services.TryAddSingleton<ITaskLogger, TaskLogger>();

            //ע�����IScheduler�¼�
            services.TryAddSingleton<ISchedulerListener, SchedulerListener>();

            //ע��Ӧ�ùر��¼�
            services.TryAddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            //ע��IJob
            var jobTypes = assembly.GetTypes().Where(m => typeof(IJob).IsImplementType(m)).ToList();
            if (jobTypes.Any())
            {
                foreach (var type in jobTypes)
                {
                    //������͵ķ���
                    services.AddTransient(type);
                }
            }

            //��������
            var taskDescriptor = new TaskDescriptor
            {
                //ģ������
                ModuleCode = "Scheduler",
                //��ʾ����
                DisplayName = "HttpTask",
                //����
                ClassFullName = $"{typeof(HttpTask).FullName}, {typeof(HttpTask).Assembly.GetName().Name}"
            };
            //��ӵ������б�
            TaskCollection.Add(taskDescriptor);
            //ע�����ģ�鼯��
            services.AddSingleton<ITaskCollection>(TaskCollection);


            //ע��IConfig
            var jobTypes3 = assembly.GetTypes().Where(m => typeof(IConfig).IsImplementType(m)).ToList();
            if (jobTypes3.Any())
            {
                foreach (var type in jobTypes3)
                {
                    //������͵ķ���
                    services.AddSingleton(type);
                }
            }

            //ע��IQuartzServer
            var jobTypes4 = assembly.GetTypes().Where(m => typeof(IQuartzServer).IsImplementType(m)).ToList();
            if (jobTypes4.Any())
            {
                foreach (var type in jobTypes4)
                {
                    //������͵ķ���
                    services.AddSingleton(type);
                }
            }

            //��ȡProvider
            _serviceProvider = services.BuildServiceProvider();

            var task1 = _serviceProvider.GetService<HttpTask>();
            var task2 = _serviceProvider.GetService<MyTask>();
            //var logger = _serviceProvider.GetService<TaskLogger>();

            //�Զ���ϵͳ����
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
        /// ���������б�
        /// </summary>
        [Fact]
        public void TaskCollectionTest()
        {
            var count = TaskCollection.Count > 0;
            Assert.True(count);
        }

        /// <summary>
        /// ���Դ���HttpTaskʵ��
        /// </summary>
        [Fact]
        public void CreateInstanceTest()
        {
            HttpTask? jobType = new(_logger);
            Assert.NotNull(jobType);
        }



        /// <summary>
        /// ����HttpTaskʵ����
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
        /// �����������ɹ�
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddHttpJobTest()
        {

            //ָ��HttpTaskʵ����
            var jobClassType = Type.GetType(_HttpTaskClass);
            if (jobClassType == null)
                Assert.Fail($"������({_HttpTaskClass})������");

            //ָ��key
            var jobKey = new JobKey(entity.Code, entity.Group);

            if (_quartzServer != null)
            {
                var jobDetail = await _quartzServer.GetJobDetail(jobKey);
                if (jobDetail != null)
                    return;
            }

            //����Job
            var job = JobBuilder.Create(jobClassType)
                .WithIdentity(jobKey)
                .UsingJobData("id", Guid.NewGuid())
                .Build();

            //����������
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(entity.Code, entity.Group)
                .EndAt(entity.EndDate.ToUniversalTime())
                .WithDescription(entity.Name);

            //�����ʼ����С�ڵ��ڵ�ǰ��������������
            if (entity.BeginDate <= DateTime.Now)
            {
                triggerBuilder.StartNow();
            }
            else
            {
                triggerBuilder.StartAt(entity.BeginDate.ToUniversalTime());
            }

            //������
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
            //CRON����
            else
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                    Assert.Fail($"CRON���ʽ��Ч");

                //CRON����
                triggerBuilder.WithCronSchedule(entity.Cron);
            }

            var trigger = triggerBuilder.Build();

            try
            {
                var offset = await _quartzServer.AddJob(job, trigger);
            }
            catch (Exception ex)
            {
                Assert.Fail($"��������������ʧ�ܣ�{ex}");
            }

            _output.WriteLine($"�������ɹ�");
        }


        /// <summary>
        /// �����������ɹ�
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddMyTaskTest()
        {
            //ָ��HttpTaskʵ����
            var jobClassType = Type.GetType(_MyTaskClass);
            if (jobClassType == null)
                Assert.Fail($"������({_MyTaskClass})������");

            //ָ��key
            var jobKey = new JobKey(entity.Code, entity.Group);

            if (_quartzServer != null)
            {
                var jobDetail = await _quartzServer.GetJobDetail(jobKey);
                if (jobDetail != null)
                    return;
            }

            //����Job
            var job = JobBuilder.Create<MyTask>()
                .WithIdentity(jobKey)
                .UsingJobData("id", Guid.NewGuid())
                .Build();

            //����������
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(entity.Code, entity.Group)
                .EndAt(entity.EndDate.ToUniversalTime())
                .WithDescription(entity.Name);

            //��������
            triggerBuilder.StartNow();

            //������
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
            //CRON����
            else
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                    Assert.Fail($"CRON���ʽ��Ч");

                //CRON����
                triggerBuilder.WithCronSchedule(entity.Cron);
            }

            var trigger = triggerBuilder.Build();

            try
            {
                var offset = await _quartzServer.AddJob(job, trigger);
            }
            catch (Exception ex)
            {
                Assert.Fail($"��������������ʧ�ܣ�{ex}");
            }

            _output.WriteLine($"�������ɹ�");
        }



        /// <summary>
        /// ������ͣ����
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PauseJobTest()
        {
            //ָ��key
            var jobKey = new JobKey(entity.Code, entity.Group);
            await _quartzServer.PauseJob(jobKey);
            _output.WriteLine($"��ͣ����ɹ�");
        }


        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task StartJobTest()
        {
            await _quartzServer.Start();

            ///�ȴ�10��
            await Task.Delay(20000);

            _output.WriteLine($"��������ɹ�");

        }
    }
}