using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Core;
using CRB.TPM.Data.Core.Internal;
using CRB.TPM.Data.Core.Sharding;

//ReadWriteSeparation
using CRB.TPM.Data.Core.ReadWriteSeparation;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加CRB.TPM数据库核心
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">自定义配置项委托</param>
    /// <returns></returns>
    public static IDbBuilder AddCRBTPMDb<TDbContext, TClientDbContext>(this IServiceCollection services, Action<DbOptions> configure = null)
        where TDbContext : IDbContext 
        where TClientDbContext : IClientDbContext
    {
        return services.AddCRBTPMDb(typeof(TDbContext), configure, typeof(TClientDbContext));
    }


    /// <summary>
    /// 添加CRB.TPM数据库核心功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbContextType">数据库上下文类型</param>
    /// <param name="configure">自定义配置项委托</param>
    /// <param name="dbClientContextType">数据库上下文类型</param>
    /// <returns></returns>
    public static IDbBuilder AddCRBTPMDb(this IServiceCollection services, Type dbContextType, Action<DbOptions> configure = null, Type dbClientContextType = null)
    {
        var options = new DbOptions();

        configure?.Invoke(options);

        ////添加数据库上下文（客户端单例模式）
        //services.TryAddSingleton<IClientDbContext, ClientDbContext>();

        //添加仓储实例管理器
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        //尝试添加默认账户信息解析器
        services.TryAddSingleton<IOperatorResolver, DefaultOperatorResolver>();

        //尝试添加默认的数据库操作日志记录器
        services.TryAddSingleton<IDTPgerProvider, DTPgerProvider>();

        ////启用读写分离式时，注册读写分离链接创建工厂
        if (options.UseReadWriteSeparation)
        {
            //尝试添加读写分离上下文访问器
            services.TryAddSingleton<IReadWriteAccessor, ReadWriteAccessor>();

            //尝试添加读写连接器工厂
            services.TryAddSingleton<IReadWriteConnectorFactory, ReadWriteConnectorFactory>();

            //尝试添加读写分离管理器（手动指定）
            services.TryAddSingleton<IReadWriteManager, ReadWriteManager>();

            //尝试添加读写连接字符串的管理器
            services.TryAddSingleton<IReadWriteConnectionStringManager, ReadWriteConnectionStringManager>();
        }

        //创建数据构建器
        return new DbBuilder(services, options, dbContextType, dbClientContextType);
    }


    /// <summary>
    /// 添加Sharding功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">自定义配置项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddSharding(this IServiceCollection services, Action<DbOptions> configure = null)
    {
        var options = new DbOptions();
        configure?.Invoke(options);

        //启用读写分离式时，注册读写分离链接创建工厂
        if (options.UseReadWriteSeparation)
        {
            //尝试添加读写分离上下文访问器
            services.TryAddSingleton<IReadWriteAccessor, ReadWriteAccessor>();

            //尝试添加读写连接器工厂
            services.TryAddSingleton<IReadWriteConnectorFactory, ReadWriteConnectorFactory>();

            //尝试添加读写分离管理器（手动指定）
            services.TryAddSingleton<IReadWriteManager, ReadWriteManager>();

            //尝试添加读写连接字符串的管理器
            services.TryAddSingleton<IReadWriteConnectionStringManager, ReadWriteConnectionStringManager>();
        }

        return services;
    }
}