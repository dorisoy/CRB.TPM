using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

[assembly: TestCaseOrderer("Cache.Redis.Tests.XUnitExtensions.PriorityOrderer", "Cache.Redis.Tests")]

namespace Cache.Redis.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.Configure<RedisOptions>(configuration.GetSection("CRB.TPM:Cache:Redis"));
                services.TryAddSingleton<IRedisSerializer, DefaultRedisSerializer>();
                services.AddSingleton<RedisHelper>();
                services.AddSingleton<ICacheProvider, RedisCacheProvider>();
            });
        }
            

        public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
    }
}