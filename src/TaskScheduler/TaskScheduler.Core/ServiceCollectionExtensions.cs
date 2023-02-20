using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.TaskScheduler.Core.Quartz;
using CRB.TPM.Utils.App;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Quartz;

namespace CRB.TPM.TaskScheduler.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加任务调度功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IServiceCollection AddTaskScheduler(this IServiceCollection services, IModuleCollection modules)
        {
            var taskCollection = new TaskCollection();

            //反射注入所有模块的任务服务
            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    //获取ITask抽象任务实现
                    var jobTypes = module.LayerAssemblies.Core.GetTypes().Where(m => typeof(ITask).IsImplementType(m)).ToList();
                    if (jobTypes.Any())
                    {
                        foreach (var jobType in jobTypes)
                        {
                            // 排除HttpTask
                            if (!jobType.Name.Equals("HttpTask"))
                            {
                                var taskDescriptor = new TaskDescriptor
                                {
                                    ModuleCode = module.Code,
                                    DisplayName = jobType.Name,
                                    ClassFullName = $"{jobType.FullName}, {jobType.Assembly.GetName().Name}"
                                };

                                var displayNameAttribute = jobType.GetCustomAttribute(typeof(DisplayNameAttribute));
                                if (displayNameAttribute != null)
                                {
                                    taskDescriptor.DisplayName = ((DisplayNameAttribute)displayNameAttribute).DisplayName;
                                }

                                taskCollection.Add(taskDescriptor);
                            }

                            services.AddTransient(jobType);
                        }
                    }
                }
            }

            //注入日志
            services.TryAddSingleton<ITaskLogger, TaskLogger>();

            //注入调度模块集合
            services.AddSingleton<ITaskCollection>(taskCollection);

            //注入应用关闭事件
            services.TryAddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            return services;
        }



        /// <summary>
        /// 添加任务调度功能
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, IModuleCollection modules)
        {
            var collection = new QuartzModuleCollection();

            //反射注入所有模块的任务服务
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
                        //获取ITask抽象任务实现
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

                            System.Diagnostics.Debug.WriteLine($"{taskType.ToString()}");

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

            //注入日志
            services.TryAddSingleton<ITaskLogger, TaskLogger>();

            //注入调度模块集合
            services.AddSingleton<IQuartzModuleCollection>(collection);

            //注入应用关闭事件
            services.TryAddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            return services;
        }
    }
}
