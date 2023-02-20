using CRB.TPM.MessageQueue.RabbitMQ;
using CRB.TPM.Utils.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

[assembly: TestCaseOrderer("MessageQueue.RabbitMQ.Tests.XUnitExtensions.PriorityOrderer", "MessageQueue.RabbitMQ.Tests")]

namespace MessageQueue.RabbitMQ.Tests
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
                services.AddSingleton<JsonHelper>();
                services.AddRabbitMQ(configuration);
            });
        }


        public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
    }
}