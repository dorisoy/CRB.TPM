using CRB.TPM.Auth.Core;
using CRB.TPM.Utils.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRB.TPM.Auth.OpenAPI;

public static class ApiKeyExtensions
{
    /// <summary>
    /// 使用ApiKey认证方案
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static CRBTPMAuthBuilder UseApiKey(this CRBTPMAuthBuilder builder, Action<ApiKeyAuthenticationOptions> configure = null)
    {
        var services = builder.Services;

        //添加ApiKey配置项
        var apiKeyOptions = builder.Configuration.Get<ApiKeyAuthenticationOptions>("CRB.TPM:Auth:ApiKey");
        services.AddSingleton(apiKeyOptions);

        //添加身份认证服务
        services.AddAuthentication(options =>  
        {
            options.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;

        }).AddApiKey<ApiKeyAuthenticationService>(options =>
        {
            //builder.Configuration.Bind("ApiKeyAuth", options)
            options = apiKeyOptions;
        });

        return builder;
    }
}