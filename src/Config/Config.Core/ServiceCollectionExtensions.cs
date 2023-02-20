using CRB.TPM.Cache.Redis;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Config.Core.Provider;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Module.Abstractions.Options;
using CRB.TPM.Utils.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRB.TPM.Config.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加配置功能（默认）
    /// </summary>
    /// <returns></returns>
    public static void AddConfig(this IServiceCollection services, IConfiguration cfg, IModuleCollection modules)
    {
        if (services.Any(m => m.ServiceType == typeof(IConfigProvider)))
            return;

        //配置核心服务
        if (services.All(m => m.ServiceType != typeof(IConfigCollection)))
            services.AddImplementTypes();

        //配置实例提供器
        var serviceProvider = services.BuildServiceProvider();
        var commonOptions = serviceProvider.GetService<CommonOptions>();

        //配置存储提供器
        var storageProvider = serviceProvider.GetService<IConfigStorageProvider>();
        if (storageProvider == null)
        {
            //默认添加内存配置存储实现（可以在对应的模块基础设自定义施覆盖）
            services.TryAddScoped<IConfigStorageProvider, MemoryConfigStorageProvider>();
        }

        var configCollection = serviceProvider.GetService<IConfigCollection>();
        var redisHelper = serviceProvider.GetService<RedisHelper>();
        var redisSerializer = serviceProvider.GetService<IRedisSerializer>();

        //配置提供器使用什么样的方式存储系统配置
        if (commonOptions.ConfigProvider == ConfigProvider.Redis)
        {
            //使用Redis
            var configProvider = new RedisConfigProvider(redisHelper, configCollection, redisSerializer, storageProvider, cfg);
            //添加到模块配置
            configProvider.AddModuleConfig(modules);
            services.TryAddSingleton<IConfigProvider>(configProvider);
        }
        else
        {
            //使用DB
            var configProvider = new DefaultConfigProvider(cfg, configCollection, storageProvider, serviceProvider);
            //添加到模块配置
            configProvider.AddModuleConfig(modules);
            services.TryAddSingleton<IConfigProvider>(configProvider);
        }
    }


    /// <summary>
    /// 加载所有配置实现
    /// </summary>
    /// <param name="services"></param>
    private static void AddImplementTypes(this IServiceCollection services)
    {
        var configs = new ConfigCollection();
        var assemblies = new AssemblyHelper().Load();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(m => typeof(IConfig).IsImplementType(m) && typeof(IConfig) != m);
            foreach (var implType in types)
            {
                if (implType.FullName.NotNull())
                {
                    var descriptor = new ConfigDescriptor
                    {
                        Type = implType.FullName.Contains(".Config.") ? ConfigType.Library : ConfigType.Module,
                        Code = implType.Name.Replace("Config", ""),
                        ImplementType = implType
                    };

                    configs.Add(descriptor);
                }
            }
        }

        services.AddChangedEvent(assemblies, configs);

        services.AddSingleton<IConfigCollection>(configs);
    }

    /// <summary>
    /// 注入配置变更事件
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <param name="configs"></param>
    private static void AddChangedEvent(this IServiceCollection services, List<Assembly> assemblies, ConfigCollection configs)
    {
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(m => typeof(IConfigChangeEvent<>).IsImplementType(m));
            foreach (var implType in types)
            {
                var configType = implType.GetInterfaces().First().GetGenericArguments()[0];
                var configDescriptor = configs.FirstOrDefault(m => m.ImplementType == configType);
                if (configDescriptor != null)
                {
                    configDescriptor.ChangeEvents.Add(implType);

                    services.AddSingleton(implType);
                }
            }
        }
    }
}
