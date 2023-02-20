using Castle.DynamicProxy;
using CRB.TPM;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.Descriptors;
using CRB.TPM.Data.Abstractions.Events;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using CRB.TPM.Data.Core.Internal;
using CRB.TPM.Utils.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Microsoft.Extensions.DependencyInjection;

public static class DbBuilderExtensions
{
    /// <summary>
    /// 使用数据库
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="connectionString"></param>
    /// <param name="provider"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IDbBuilder UseDb(this IDbBuilder builder, string connectionString, DbProvider provider, Action<DbOptions> configure = null)
    {
        builder.Options.ConnectionString = connectionString;
        builder.Options.Provider = provider;
        configure?.Invoke(builder.Options);

        return builder;
    }

    /// <summary>
    /// 添加CodeFirst功能
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure">自定义配置</param>
    /// <returns></returns>
    public static IDbBuilder AddCodeFirst(this IDbBuilder builder, Action<CodeFirstOptions> configure = null)
    {
        var options = new CodeFirstOptions();
        configure?.Invoke(options);

        if (options.CreateDatabase)
        {
            builder.CodeFirstOptions = options;

            //注册数据库相关事件
            builder.RegisterDatabaseEvents();

            //注册表相关事件
            builder.RegisterTableEvents();

            builder.AddAction(() =>
            {
                //优先使用自定义的代码优先提供器，毕竟默认的建库建表语句并不能满足所有人的需求
                var provider = options.CustomCodeFirstProvider ?? builder.DbContext.CodeFirstProvider;
                if (provider != null)
                {
                    //先有库
                    provider.CreateDatabase();

                    //后有表
                    provider.CreateTable();
                }
            });
        }
        return builder;
    }


    /// <summary>
    /// 添加读写分离功能
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IDbBuilder AddReadWriteSeparation(this IDbBuilder builder, Action<DbOptions> configure = null)
    {
        var options = new DbOptions();
        configure?.Invoke(options);
        builder.Options.ReadWriteSeparationOptions = options.ReadWriteSeparationOptions;
        builder.Options.UseReadWriteSeparation = options.UseReadWriteSeparation;
        return builder;
    }

    /// <summary>
    /// 添加读写分离配置
    /// </summary>
    /// <param name="builder"></param> 
    /// <param name="configure"></param>
    /// <param name="readStrategyEnum">随机或者轮询</param>
    /// <param name="defaultEnable">false表示哪怕您添加了读写分离也不会进行读写分离查询,只有需要的时候自行开启,true表示默认查询就是走的读写分离</param>
    /// <param name="defaultPriority">默认优先级建议大于0</param>
    /// <param name="readConnStringGetStrategy">LatestFirstTime:DbContext缓存,LatestEveryTime:每次都是最新</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static IDbBuilder AddReadWriteSeparation(this IDbBuilder builder, Func<IServiceCollection, IDictionary<string, IEnumerable<string>>> configure,
        ReadStrategyEnum readStrategyEnum,
        bool defaultEnable = false,
        int defaultPriority = 10,
        ReadConnStringGetStrategyEnum readConnStringGetStrategy = ReadConnStringGetStrategyEnum.LatestFirstTime)
    {
        var options  = new ReadWriteSeparationOptions();
        //configure?.Invoke(options);

        //options.ReadWriteSeparationConfigure = configure ?? throw new ArgumentNullException(nameof(configure));
        options.ReadStrategy = readStrategyEnum;
        options.DefaultEnable = defaultEnable;
        options.DefaultPriority = defaultPriority;
        options.ReadConnStringGetStrategy = readConnStringGetStrategy;

        return builder;
    }


    /// <summary>
    /// 添加事务特性功能
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceType"></param>
    /// <param name="implementationType"></param>
    /// <returns></returns>
    public static IDbBuilder AddTransactionAttribute(this IDbBuilder builder, Type serviceType, Type implementationType)
    {
        var services = builder.Services;

        //尝试添加代理生成器
        services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();

        //添加需要特性事务的服务
        services.AddScoped(serviceType, sp =>
        {
            try
            {
                var target = sp.GetService(implementationType);
                var generator = sp.GetService<IProxyGenerator>();
                var manager = sp.GetService<IRepositoryManager>();
                var interceptor = new TransactionInterceptor(builder.DbContext, manager);
                var proxy = generator!.CreateInterfaceProxyWithTarget(serviceType, target, interceptor);
                return proxy;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("IClientDbContext") >= 0)
                {
                    throw new InvalidOperationException($"你需要启用：opt.UseClientMode = true 并且自定义类 {{模块}}ClientDbContext 继承自 ClientDbContext ");
                }
                throw new InvalidOperationException($"服务注入异常：{ex.Message} {ex.StackTrace}");
            }
        });
        return builder;
    }


    /// <summary>
    /// 加载初始化数据
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    private static List<JsonProperty> LoadInitData(CodeFirstOptions options)
    {
        //初始化数据
        if (options.InitData && options.InitDataFilePath.NotNull() && File.Exists(options.InitDataFilePath))
        {
            using var jsonReader = new StreamReader(options.InitDataFilePath, Encoding.UTF8);
            var str = jsonReader.ReadToEnd();

            var doc = JsonDocument.Parse(str);
            return doc.RootElement.EnumerateObject().ToList();
        }

        return new List<JsonProperty>();
    }

    private static void InitTableData(List<JsonProperty> initData, IDbContext dbContext, IEntityDescriptor entityDescriptor, IServiceCollection services)
    {
        //初始化数据
        if (initData.Any())
        {
            if (initData.All(m => m.Name != entityDescriptor.Name))
                return;

            var property = initData.FirstOrDefault(m => m.Name == entityDescriptor.Name);

            var list = (IList)JsonHelper.Instance.Deserialize(property.Value.ToString(), typeof(List<>).MakeGenericType(entityDescriptor.EntityType));

            if (list.Count == 0)
                return;

            var repositoryDescriptor = dbContext.RepositoryDescriptors.FirstOrDefault(m => m.EntityType == entityDescriptor.EntityType);

            var repository = services.BuildServiceProvider().GetService(repositoryDescriptor!.InterfaceType);
            var repositoryType = repository.GetType();

            using var uow = dbContext.NewUnitOfWork();

            var bindingUowMethod = repositoryType.GetMethod("BindingUow");
            bindingUowMethod!.Invoke(repository, new object[] { uow });

            var bulkAddMethod = repositoryType.GetMethods().Single(m => m.Name == "BulkAdd" && m.GetParameters().Length == 3);
            var bulkAddTask = (Task)bulkAddMethod!.Invoke(repository, new object[] { list, 0, uow });
            bulkAddTask!.Wait();

            uow.SaveChanges();
        }
    }

    /// <summary>
    /// 注册数据库相关事件
    /// </summary>
    /// <param name="builder"></param>
    private static void RegisterDatabaseEvents(this IDbBuilder builder)
    {
        var databaseCreateEvents = builder.Services.BuildServiceProvider().GetServices<IDatabaseCreateEvent>();

        //创建前
        builder.CodeFirstOptions.BeforeCreateDatabase = dbContext =>
        {
            var eventContext = new DatabaseCreateContext
            {
                DbContext = dbContext,
                CreateTime = DateTime.Now
            };

            foreach (var databaseCreateEvent in databaseCreateEvents)
            {
                databaseCreateEvent.OnBeforeCreate(eventContext);
            }
        };

        //创建后
        builder.CodeFirstOptions.AfterCreateDatabase = dbContext =>
        {
            var eventContext = new DatabaseCreateContext
            {
                DbContext = dbContext,
                CreateTime = DateTime.Now
            };

            foreach (var databaseCreateEvent in databaseCreateEvents)
            {
                databaseCreateEvent.OnAfterCreate(eventContext);
            }
        };
    }

    /// <summary>
    /// 注册表创建事件
    /// </summary>
    /// <param name="builder"></param>
    private static void RegisterTableEvents(this IDbBuilder builder)
    {
        //加载初始化数据对象
        var initData = LoadInitData(builder.CodeFirstOptions);

        var tableCreateEvents = builder.Services.BuildServiceProvider().GetServices<ITableCreateEvent>();

        //创建前
        builder.CodeFirstOptions.BeforeCreateTable = (dbContext, entityDescriptor) =>
        {
            var tableCreateContext = new TableCreateContext
            {
                DbContext = dbContext,
                EntityDescriptor = entityDescriptor,
                CreateTime = DateTime.Now
            };

            foreach (var tableCreateEvent in tableCreateEvents)
            {
                tableCreateEvent.OnBeforeCreate(tableCreateContext);
            }
        };

        //创建后
        builder.CodeFirstOptions.AfterCreateTable = (dbContext, entityDescriptor) =>
        {
            //初始化表数据
            InitTableData(initData, dbContext, entityDescriptor, builder.Services);

            var tableCreateContext = new TableCreateContext
            {
                DbContext = dbContext,
                EntityDescriptor = entityDescriptor,
                CreateTime = DateTime.Now
            };

            foreach (var tableCreateEvent in tableCreateEvents)
            {
                tableCreateEvent.OnAfterCreate(tableCreateContext);
            }
        };

    }
}