using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Logging.Abstractions.Providers;
using CRB.TPM.Logging.Core.DefaultProviders;

namespace CRB.TPM.Logging.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加日志功能
    /// </summary>
    /// <param name="services"></param>
    public static void AddCRBTPMLogging(this IServiceCollection services)
    {
        //登录日志处理器
        services.AddScoped<ILoginLogHandler, LoginLogHandler>();
        //审计日志处理器
        services.AddScoped<IAuditLogHandler, AuditLogHandler>();
    }
}