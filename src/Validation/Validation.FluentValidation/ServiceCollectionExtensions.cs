using CRB.TPM.Module.Abstractions;
using CRB.TPM.Validation.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRB.TPM.Validation.FluentValidation;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加FluentValidation验证
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IMvcBuilder AddValidators(this IMvcBuilder builder, IServiceCollection services)
    {
        //注入验证结果格式化器
        services.TryAddSingleton<IValidateResultFormatHandler, ValidateResultFormatHandler>();

        //添加FluentValidation自动验证管道支持
        services.AddFluentValidationAutoValidation(fv =>
        {
            var modules = services.BuildServiceProvider().GetService<IModuleCollection>();
            foreach (var module in modules)
            {
                if (module.LayerAssemblies != null && module.LayerAssemblies is ModuleLayerAssemblies descriptor)
                {
                    var assembly = descriptor.Web ?? descriptor.Api;
                    if (assembly != null)
                    {
                        services.AddValidatorsFromAssembly(assembly);
                    }
                }
            }
        }).AddFluentValidationClientsideAdapters();


        //当一个验证失败时，后续的验证不再执行
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        return builder;
    }
}
