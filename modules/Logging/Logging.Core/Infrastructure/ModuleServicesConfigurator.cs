using CRB.TPM.Logging.Abstractions.Providers;
//using CRB.TPM.Mod.Logging.Core.Infrastructure.LoginHandler;
using CRB.TPM.Module.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CRB.TPM.Mod.Logging.Core.Infrastructure;

public class ModuleServicesConfigurator : IModuleServicesConfigurator
{
    public void PreConfigure(ModuleConfigureContext context)
    {
      
    }

    public void Configure(ModuleConfigureContext context)
    {
    }

    public void PostConfigure(ModuleConfigureContext context)
    {
        //登录日志处理器
        //context.Services.AddSingleton<ILoginLogHandler, DefaultLoginLogHandler>();
    }
}