using CRB.TPM.Data.Abstractions.Events;
using CRB.TPM.Mod.Scheduler.Core.Infrastructure.Core;
using CRB.TPM.Mod.Scheduler.Core.Infrastructure.Defaults;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core;
using CRB.TPM.TaskScheduler.Core.Quartz;
using CRB.TPM.Utils.App;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.ComponentModel;
using System.Linq;
using TaskLogger = CRB.TPM.Mod.Scheduler.Core.Infrastructure.Core.TaskLogger;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace CRB.TPM.Mod.Logging.Core.Infrastructure;

public class ModuleServicesConfigurator : IModuleServicesConfigurator
{
    /// <summary>
    /// 前置服务注入
    /// </summary>
    /// <param name="context"></param>
    public void PreConfigure(ModuleConfigureContext context)
    {
        //注册数据库创建事件，注意：CreateEvent 一定要先于AddData() 前注册，因为Event的执行在Codefirst中触发
        context.Services.TryAddSingleton<IDatabaseCreateEvent, CreateDatabaseEvent>();
    }

    /// <summary>
    /// 服务注入
    /// </summary>
    /// <param name="context"></param>
    public void Configure(ModuleConfigureContext context)
    {
        context.Services.Configure<QuartzOptions>(options =>
        {
            options.Scheduling.IgnoreDuplicates = true; 
            options.Scheduling.OverWriteExistingData = true; 
        });

        //（测试）
        //context.Services.AddQuartzTest(context.Modules);

        //默认标准：CRB.TPM.Mod.XX.Core.Application.Tasks 下实现TaskAbstract 的注册方式
        context.Services.AddTaskScheduler(context.Modules);

        //扩展标准：CRB.TPM.Mod.XX.Quartz 使用Quartz独立程序集下实现TaskAbstract 时注册方式
        context.Services.AddQuartz(context.Modules);

    }

    /// <summary>
    /// 后置服务注入
    /// </summary>
    /// <param name="context"></param>
    public void PostConfigure(ModuleConfigureContext context)
    {
        //注入日志
        context.Services.TryAddSingleton<ITaskLogger,TaskLogger>();
        //注入监听IScheduler事件
        context.Services.TryAddSingleton<ISchedulerListener, SchedulerListener>();
        //注入Quartz服务实例
        context.Services.TryAddSingleton<IQuartzServer, SchedulerServer>();
        //注入应用关闭事件
        context.Services.TryAddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();
    }
}

#if DEBUG
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加Quartz服务测试
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modules">模块集合</param>
    /// <returns></returns>
    public static IServiceCollection AddQuartzTest(this IServiceCollection services, IModuleCollection modules)
    {
        var collection = new QuartzModuleCollection();

        #region ==反射注入所有模块的任务服务==

        if (modules != null && modules.Any())
        {
            foreach (var module in modules)
            {
                var descriptor = new QuartzModuleDescriptor
                {
                    Module = module
                };

                var quartzAssembly = module.LayerAssemblies.Quartz;
                if (quartzAssembly != null)
                {
                    var taskTypes = module.LayerAssemblies.Quartz.GetTypes().Where(m => typeof(ITask).IsAssignableFrom(m));
                    foreach (var taskType in taskTypes)
                    {
                        //排除HttpTask
                        if (!taskType.Name.Equals("HttpTask"))
                        {
                            var taskDescriptor = new QuartzTaskDescriptor
                            {
                                Name = taskType.Name,
                                Class = $"{taskType.FullName}, {taskType.Assembly.GetName().Name}"
                            };

                            var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(taskType, typeof(DescriptionAttribute));
                            if (descAttr != null && descAttr.Description.NotNull())
                            {
                                taskDescriptor.Name = descAttr.Description;
                            }

                            descriptor.Tasks.Add(taskDescriptor);
                        }

                        services.AddTransient(taskType);
                    }
                }

                foreach (var task in descriptor.Tasks)
                {
                    descriptor.TaskSelect.Add(new OptionResultModel
                    {
                        Label = task.Name,
                        Value = task.Class
                    });
                }

                collection.Add(descriptor);
            }
        }

        #endregion

        //注入调度模块集合
        services.AddSingleton<IQuartzModuleCollection>(collection);

        return services;
    }
}
#endif