using System;
using CRB.TPM.Module.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure;

public class ModuleServicesConfigurator : IModuleServicesConfigurator
{
    public void PreConfigure(ModuleConfigureContext context)
    {
    }

    public void Configure(ModuleConfigureContext context)
    {
        //注入当前模块中特有的服务
    }

    public void PostConfigure(ModuleConfigureContext context)
    {
        
    }
}