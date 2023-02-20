using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Collections.Generic;
using CRB.TPM.Host.Web.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using CRB.TPM.Cache.Redis;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Core;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Excel.Core;
using CRB.TPM.Excel.EPPlus;
using CRB.TPM.Host.Web.BackgroundServices;
using CRB.TPM.Host.Web.Filters;
using CRB.TPM.Host.Web.Swagger.Conventions;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Module.Web;
using CRB.TPM.Utils.Json;
using CRB.TPM.Utils.Json.Converters;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using System.Xml.Linq;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Module.Abstractions.Options;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Utils.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.StackExchangeRedis;
using Microsoft.AspNetCore.Http;
using CRB.TPM.Utils.Http;
using CRB.TPM.Host.Web.Helpers;
using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Cache.Core;
using CRB.TPM.Utils.Web.Helpers;
using CRB.TPM.Utils.Abstracts;
using CRB.TPM.Utils.Web;
using System.Text.Json.Serialization;


namespace CRB.TPM.Host.Web;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加MVC功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modules"></param>
    /// <param name="hostOptions"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    public static IMvcBuilder AddMvc(this IServiceCollection services, IModuleCollection modules, Options.HostOptions hostOptions, IHostEnvironment env)
    {
        //添加多语言支持
        services.AddLocalization(opt => opt.ResourcesPath = "Resources");

        var mvcBuilder = services.AddMvc(c =>
            {
                if (hostOptions!.Swagger || !env.IsProduction())
                {
                    //API分组约定
                    c.Conventions.Add(new ApiExplorerGroupConvention());
                }

                //审计日志全局过滤器
                c.Filters.Add(typeof(AuditFilterAttribute));

            })
            .AddJsonOptions(options =>
            {
                //不区分大小写的反序列化
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //忽略循环引用
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                //ReferenceHandler.Preserve
                //属性名称使用 camel 大小写
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                //最大限度减少字符转义
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                //添加日期转换器
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                //添加元组（ValueTuples）支持
                options.JsonSerializerOptions.Converters.Add(new ValueTupleFactory());
                 //添加System.Type类型支持
                options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
                //添加IConfig类型支持
                options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForIConfig());
                //忽略空字段
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                //添加多态嵌套序列化
                options.JsonSerializerOptions.AddPolymorphism();
            })
            //多语言
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var module = modules.FirstOrDefault(m => m.LayerAssemblies.Core == type.Assembly);
                    if (module != null && module.LocalizerType != null)
                    {
                        return factory.Create(module.LocalizerType);
                    }

                    return factory.Create(type);
                };
            });

        //添加模块
        mvcBuilder.AddModules(modules);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加CORS
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hostOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddCors(this IServiceCollection services, Options.HostOptions hostOptions)
    {
        services.AddCors(options =>
        {
            /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                    所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                    第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                    Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
            var preflightMaxAge = hostOptions.PreflightMaxAge > 0 ? new TimeSpan(0, 0, hostOptions.PreflightMaxAge) : new TimeSpan(0, 30, 0);

            //添加默认策略（不允许）
            options.AddPolicy("Default",
                builder => builder.SetIsOriginAllowed(_ => true)
                    .SetPreflightMaxAge(preflightMaxAge)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面

            //允许跨域策略
            options.AddPolicy("NoPolicy",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                   });
        });

        return services;
    }

    /// <summary>
    /// 添加数据库
    /// </summary>
    /// <param name="services"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static IServiceCollection AddData(this IServiceCollection services, IModuleCollection modules)
    {
        var prd = services.BuildServiceProvider();
        var commonOptions = prd.GetRequiredService<CommonOptions>();

        //遍历模块
        foreach (var module in modules)
        {
            var dbOptions = module.Options!.Db;

            //模块分层程序集
            //eg. dbContextType = {Name = "AdminDbContext" FullName = "CRB.TPM.Mod.Admin.Core.Infrastructure.AdminDbContext"}

            var dbContextType = module.LayerAssemblies.Core.GetTypes().FirstOrDefault(m => typeof(DbContext).IsAssignableFrom(m));
            var dbClientContextType = module.LayerAssemblies.Core.GetTypes().FirstOrDefault(m => typeof(ClientDbContext).IsAssignableFrom(m));

            Check.NotNull(dbContextType, $"基础设施缺失 {module.Code}DbContext 上下文，解决此问题，需要实现 “ModuleDbContext : DbContext”。");
            Check.NotNull(dbContextType, $"基础设施缺失 {module.Code}DbContext 上下文，解决此问题，需要实现 “ModuleClientDbContext : ClientDbContext”。");

            //为数据构建器添加CRB.TPM数据库核心功能
            var dbBuilder = services.AddCRBTPMDb(dbContextType, opt =>
            {
                //配置到模块
                opt.UseClientMode = commonOptions.UseClientMode;

                opt.Provider = dbOptions.Provider;
                opt.UseReadWriteSeparation = dbOptions.UseReadWriteSeparation;
                opt.ReadWriteSeparationOptions = dbOptions.ReadWriteSeparationOptions;

                //Sqlite数据库自动创建数据库文件
                if (dbOptions.ConnectionString.IsNull() && dbOptions.Provider == DbProvider.Sqlite)
                {
                    string dbFilePath = Path.Combine(AppContext.BaseDirectory, "db");
                    if (!Directory.Exists(dbFilePath))
                    {
                        Directory.CreateDirectory(dbFilePath);
                    }
                    dbOptions.ConnectionString = $"Data Source={dbFilePath}/{module.Code}.db;Mode=ReadWriteCreate";
                }

                //模块默认连接替换全局数据库连接
                opt.ConnectionString = dbOptions.ConnectionString;

                //模块日志开启则激活全局日志开启
                opt.Log = dbOptions.Log;

                //模块表前缀替换全局表前缀
                opt.TableNamePrefix = dbOptions.TableNamePrefix;

                //模块表分隔符替换全局表分隔符
                opt.TableNameSeparator = dbOptions.TableNameSeparator;

                //模块数据库版本替换全局数据库版本
                opt.Version = dbOptions.Version;

                //模块编码替换全局模块编码
                opt.ModuleCode = module.Code;

            }, dbClientContextType);

            //数据构建器加载仓储
            dbBuilder.AddRepositoriesFromAssembly(module.LayerAssemblies.Core);

            //数据构建器添加读写分离
            if (dbOptions.UseReadWriteSeparation)
            {
                dbBuilder.AddReadWriteSeparation(opt =>
                {
                    //启用全局读写分类离则默认连接失效
                    opt.ConnectionString = "";
                    //全局是否使用读写分离
                    opt.UseReadWriteSeparation = dbOptions.UseReadWriteSeparation;
                    //模块读写连接配置替换全局连接配置
                    opt.ReadWriteSeparationOptions = dbOptions.ReadWriteSeparationOptions;
                });
            }

            //数据构建器添加代码优先
            if (dbOptions.CodeFirst)
            {
                dbBuilder.AddCodeFirst(opt =>
                {
                    opt.CodeFirst = dbOptions.CodeFirst;
                    opt.CreateDatabase = dbOptions.CreateDatabase;
                    opt.UpdateColumn = dbOptions.UpdateColumn;
                    opt.InitData = dbOptions.InitData;
                    opt.InitDataFilePath = module.DbInitFilePath;
                });
            }

            //数据构建器添加特性事务
            foreach (var dic in module.ApplicationServices)
            {
                dbBuilder.AddTransactionAttribute(dic.Key, dic.Value);
            }

            //构建数据构建器
            dbBuilder.Build();
        }

        return services;
    }

    /// <summary>
    /// 添加后台服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        //添加创建表后台服务
        services.AddHostedService<CreateTableBackgroundService>();

        return services;
    }

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cfg"></param>
    /// <returns></returns>
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration cfg)
    {
        var builder = services.AddCache();

        var provider = cfg["CRB.TPM:Cache:Provider"];

        if (provider != null && (provider.ToLower() == "redis"|| provider.ToLower() == "1"))
        {
            builder.UseRedis(cfg);
        }

        builder.Build();

        return services;
    }

    /// <summary>
    /// 添加Excel
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cfg"></param>
    /// <returns></returns>
    public static IServiceCollection AddExcel(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<ExcelOptions>(cfg.GetSection("CRB.TPM:Excel"));
        services.AddExcel(builder =>
        {
            var options = services.BuildServiceProvider().GetService<IOptions<ExcelOptions>>().Value;
            if (options.Provider == ExcelProvider.EPPlus)
            {
                builder.UseEPPlus();
            }
        });

        return services;
    }


    /// <summary>
    /// 添加数据保护
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cfg"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataProtection(this IServiceCollection services, IConfiguration cfg)
    {

        //添加数据保护,检查是否在redis中保留数据 
        var config = services.BuildServiceProvider().GetService<IOptions<RedisOptions>>().Value;
        if (config != null && config.UseRedisToStoreDataProtectionKeys)
        {
            //秘钥存储到Redis中
            services.AddDataProtection().PersistKeysToStackExchangeRedis(() =>
            {
                var redisConnectionWrapper = services.BuildServiceProvider().GetRequiredService<RedisHelper>();
                return redisConnectionWrapper.GetDb(config.DefaultDb);

            }, CachingDefaults.RedisDataProtectionKey);
        }
        else
        {
            //使用本地目录存储
            var fileProvider = services.BuildServiceProvider().GetService<ITPMFileProvider>();
            var dataProtectionKeysPath = fileProvider.MapPath("~/App_Data/DataProtectionKeys");
            var dataProtectionKeysFolder = new System.IO.DirectoryInfo(dataProtectionKeysPath);
            services.AddDataProtection().PersistKeysToFileSystem(dataProtectionKeysFolder);
        }
        return services;
    }


    /// <summary>
    /// 添加防伪支持所需的服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hostOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddAntiForgery(this IServiceCollection services, Options.HostOptions hostOptions)
    {
        var config = services.BuildServiceProvider().GetService<IOptions<CommonOptions>>().Value;
        //覆盖 cookie
        services.AddAntiforgery(options =>
        {
            //AngularJS 配置防伪服务查找名为X-XSRF-TOKEN的请求头
            options.HeaderName = HttpDefaults.XXSRFTOKENForHeader;
            options.Cookie.Name = $"{CookieDefaults.Prefix}{CookieDefaults.AntiforgeryCookie}";
            //是否允许在其他不受SSL保护的存储页面上使用来自SSL保护页面的防伪cookie
            options.Cookie.SecurePolicy = config.ForceSslForAllPages ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
        });

        return services;
    }


    /// <summary>
    /// 添加Web辅助器
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWebHelper(this IServiceCollection services)
    {
        //文件提供
        services.AddScoped<ITPMFileProvider, TPMFileProvider>();

        //添加Web辅助器
        services.AddScoped<IWebHelper, WebHelper>();

        return services;
    }


    /// <summary>
    /// 添加OSS功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cfg"></param>
    /// <returns></returns>
    public static IServiceCollection AddOSS(this IServiceCollection services, IConfiguration cfg)
    {
        var config = new OSSOptions();
        var section = cfg.GetSection("OSS");
        if (section != null)
        {
            section.Bind(config);
        }

        if (config.CRB != null && config.CRB.Domain.NotNull() && !config.CRB.Domain.EndsWith("/"))
        {
            config.CRB.Domain += "/";
        }
        if (config.CRC != null && config.CRC.Domain.NotNull() && !config.CRC.Domain.EndsWith("/"))
        {
            config.CRC.Domain += "/";
        }

        services.AddSingleton(config);

        //这里将来独立雪花云或者华润云存储到单独的程序集时使用，如：CRB.TPM.OSS.CRB or CRB.TPM.OSS.CRC
        //var assembly = AssemblyHelper.LoadByNameEndString($".TPM.OSS.{config.Provider.ToString()}");
        //if (assembly == null)
        //    return services;

        var assembly = new AssemblyHelper().GetCurrentAssembly();
        var providerType = assembly.GetTypes().FirstOrDefault(m => typeof(IFileStorageProvider).IsAssignableFrom(m));
        if (providerType != null)
        {
            services.AddSingleton(typeof(IFileStorageProvider), providerType);
        }

        return services;
    }
}