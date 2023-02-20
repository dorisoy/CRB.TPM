using DotNetCore.CAP;
using System;
using Savorboard.CAP.InMemoryMessageQueue;
using CRB.TPM.CAP.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加Cap服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IServiceCollection AddCap(this IServiceCollection services, Action<CAPOptions> action)
    {
        var options = new CAPOptions();
        if (action != null)
        {
            action.Invoke(options);
        }

        services.AddCap(x =>
        {
            if (options.Provider == CAPProvider.SqlServer)
            {
                x.UseSqlServer(options.ConnectionStrings);
            }
            else if (options.Provider == CAPProvider.MySql)
            {
                x.UseMySql(options.ConnectionStrings);
            }
            else
            {
                x.UseInMemoryStorage();
            }

            if (options.MQProvider ==  CAPMQProvider.RabbitMQ)
            {
                x.UseRabbitMQ(options.MQConnectionStrings);
            }
            else
            {
                x.UseInMemoryMessageQueue();
            }
        });

        return services;
    }
}