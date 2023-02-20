using CRB.TPM.Config.Abstractions;
using CRB.TPM.Config.Core;
using CRB.TPM.Logging.Abstractions.Providers;
using CRB.TPM.Mod.Admin.Core.Infrastructure.LoginHandler;
using CRB.TPM.Module.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure;

public class ModuleServicesConfigurator : IModuleServicesConfigurator
{
    public void PreConfigure(ModuleConfigureContext context)
    {
     
    }

    public void Configure(ModuleConfigureContext context)
    {
        //添加内存配置存储实现
        context.Services.AddSingleton<IConfigStorageProvider, AdminConfigStorageProvider>();
    }

    public void PostConfigure(ModuleConfigureContext context)
    {
        //登录日志处理器
        context.Services.AddSingleton<ILoginLogHandler, DefaultLoginLogHandler>();
    }
}