using Divergic.Logging.Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Xunit.Abstractions;

namespace SP.Tests
{
    public class WebTestFixture : WebApplicationFactory<WebHost.Program>
    {
        public WebTestFixture()
        {

        }
        public ITestOutputHelper? Output { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.UseEnvironment("Development");
            base.ConfigureWebHost(builder);
            builder.ConfigureLogging(logging =>
            {
                if (Output != null)
                {
                    logging.ClearProviders();
                    logging.AddXunit(Output, new LoggingConfig
                    {
                        LogLevel = LogLevel.Trace
                    });
                }
            });

        }

        //ITestOutputHelper 在 Test 类的构造函数中设置
        public WebTestFixture SetOutPut(ITestOutputHelper output)
        {
            Output = output;
            return this;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Output = null;
        }
    }
}
