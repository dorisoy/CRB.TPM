using CRB.TPM.Config.Core;
using CRB.TPM.Host.Web.Middleware;
using CRB.TPM.Host.Web.Swagger;
using CRB.TPM.Logging.Core;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Module.Core;
using CRB.TPM.Module.Web;
using CRB.TPM.Utils;
using CRB.TPM.Utils.Helpers;
using CRB.TPM.Utils.Web;
using CRB.TPM.Auth.OpenAPI;
using CRB.TPM.Validation.FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using HostOptions = CRB.TPM.Host.Web.Options.HostOptions;

namespace CRB.TPM.Host.Web;

/// <summary>
/// 宿主启动器
/// </summary>
public class HostBootstrap
{
    /// <summary>
    /// 启动应用
    /// </summary>
    /// <returns></returns>
    public void Run(string[] args)
    {
        var options = LoadOptions();

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseDefaultServiceProvider(opt => { opt.ValidateOnBuild = false; });

        //使用Serilog日志
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom
                .Configuration(hostingContext.Configuration)
                .Enrich
                .FromLogContext();
        });

        //绑定URL
        builder.WebHost.UseUrls(options.Urls);

        var services = builder.Services;

        var env = builder.Environment;
        var cfg = builder.Configuration;

        var modules = ConfigureServices(services, env, cfg, options);

        var app = builder.Build();

        Configure(app, modules, options, env);

        app.Run();
    }

    /// <summary>
    /// 配置服务(注意：以下注册顺序谨慎调整)
    /// </summary>
    private IModuleCollection ConfigureServices(IServiceCollection services, 
        IWebHostEnvironment env, IConfiguration cfg, HostOptions options)
    {
        services.AddSingleton(options);

        //注入Utils
        services.AddUtils(cfg);

        //添加模块（加载CRB.TPM:Common 下通用配置）
        var modules = services.AddModulesCore(env, cfg);

        //添加模块的前置服务
        services.AddModulePreServices(modules, env, cfg);

        //添加Swagger
        services.AddSwagger(modules, options, env);

        //添加缓存
        services.AddCache(cfg);

        //添加对象映射
        services.AddMappers(modules);

        //添加MVC配置
        services.AddMvc(modules, options, env)
            //添加验证器
            .AddValidators(services); 

        //添加CORS
        services.AddCors(options);

        //解决Multipart body length limit 134217728 exceeded
        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
        });

        //添加HttpClient相关
        services.AddHttpClient();

        //添加日志功能
        services.AddCRBTPMLogging();

        //添加模块的自定义特有的服务
        services.AddModuleServices(modules, env, cfg);

        //添加身份认证和授权
        services.AddCRBTPMAuth(cfg)
            .UseJwt();
            //.UseApiKey();

        //添加数据库
        services.AddData(modules);

        //添加配置功能
        services.AddConfig(cfg, modules);

        //添加后台服务
        services.AddBackgroundServices();

        //excel导出配置
        services.AddExcel(cfg);

        //添加模块的后置服务
        services.AddModulePostServices(modules, env, cfg);

        //添加Web辅助器
        services.AddWebHelper();

        //添加数据保护
        services.AddDataProtection(cfg);

        //添加Anti-forgery
        services.AddAntiForgery(options);

        //添加OSS支持
        services.AddOSS(cfg);

        return modules;
    }

    /// <summary>
    /// 配置中间件
    /// </summary>
    private void Configure(WebApplication app, IModuleCollection modules, HostOptions options, IWebHostEnvironment env)
    {
        //使用全局异常处理中间件
        app.UseMiddleware<ExceptionHandleMiddleware>();

        //基地址
        app.UsePathBase(options);

        //开放目录
        if (options.OpenDirs != null && options.OpenDirs.Any())
        {
            //设置默认页
            app.UseDefaultPage(options);

            options.OpenDirs.ForEach(m =>
            {
                app.OpenDir(m, env);
            });

            //创建默认文件提供器
            if (!string.IsNullOrEmpty(env.WebRootPath))
            {
                CommonHelper.DefaultFileProvider = new TPMFileProvider(env);
            }
        }

        //反向代理
        if (options!.Proxy)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        //路由
        app.UseRouting();

        //使用默认CORS策略，注意：UseCors 须置于UseRouting之后， UseAuthorization之前  
        if (options.NoCorsPolicy)
        {
            //允许跨域
            app.UseCors("NoPolicy");
        }
        else 
        {
            //使用跨域策略（默认）
            app.UseCors("Default");
        }
       
        //多语言
        app.UseLocalization();

        //认证
        app.UseAuthentication();

        //授权
        app.UseAuthorization();

        //配置端点
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //启用Swagger
        app.UseSwagger(modules, options, app.Environment);

        //使用模块化
        app.UseModules(modules);

        //启用Banner图
        app.UseBanner(app.Lifetime, options);

        //启用应用关闭处理
        app.UseShutdownHandler();
    }

    /// <summary>
    /// 加载宿主配置项
    /// </summary>
    /// <returns></returns>
    private HostOptions LoadOptions()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false);

        var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environmentVariable.NotNull())
        {
            configBuilder.AddJsonFile($"appsettings.{environmentVariable}.json", false);
        }

        var config = configBuilder.Build();

        var hostOptions = new HostOptions();
        config.GetSection("Host").Bind(hostOptions);

        //设置默认端口
        if (hostOptions.Urls.IsNull())
            hostOptions.Urls = "http://*:6220";

        return hostOptions;
    }
}