using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using CRB.TPM.Module.Abstractions;

namespace CRB.TPM.Module.Web;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 使用模块的中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="modules">模块集合</param>
    /// <returns></returns>
    public static IApplicationBuilder UseModules(this IApplicationBuilder app, IModuleCollection modules)
    {

        foreach (var module in modules)
        {
            if (module?.LayerAssemblies == null)
                continue;

            var assembly = module.LayerAssemblies.Web ?? module.LayerAssemblies.Api;
            if (assembly == null)
                continue;

            var middlewareConfigurator = assembly.GetTypes().FirstOrDefault(m => typeof(IModuleMiddlewareConfigurator).IsAssignableFrom(m));
            if (middlewareConfigurator != null)
            {
                //配置中间件
                ((IModuleMiddlewareConfigurator)Activator.CreateInstance(middlewareConfigurator))?.Configure(app);
            }
        }

        return app;
    }
}