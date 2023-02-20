using Divergic.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop.Implementation;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace SP.Tests
{
    public class BasicTests : IClassFixture<WebTestFixture>, IDisposable
    {
        protected readonly WebApplicationFactory<WebHost.Program> _factory;
        protected readonly ITestOutputHelper _output;
        protected readonly HttpClient _client;
        protected readonly IServiceScope _scope;
        protected Stopwatch sw = new Stopwatch();

        public BasicTests(WebTestFixture factory, ITestOutputHelper output)
        {
            _factory = factory.SetOutPut(output);
            _scope = _factory.Services.CreateScope();
            _client = _factory.CreateClient();
            _output = output;  
        }

        public async Task Login(string uname, string pwd)
        {
            _output.WriteLine($"模拟登陆【开始】，帐号：{uname}，密码：{pwd}");
            var parm = new StringContent(
                JsonSerializer.Serialize(new
                {
                    username = uname,
                    password = pwd
                }),
                Encoding.UTF8,
                Application.Json);

            var response = await _client.PostAsync("/api/Admin/Authorize/Login", parm);
            response.EnsureSuccessStatusCode();
            var jsonStr = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"模拟登陆【响应报文】，{jsonStr}");
            var jd = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonStr);
            var accessToken = jd.data.accessToken.ToString();
            _output.WriteLine($"模拟登陆【AccessToken】，{accessToken}");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        }

        public void WatchStart()
        {
            sw.Start();
        }
        public void WatchStop()
        {
            sw.Stop();
        }
        public void WatchReset()
        {
            sw.Reset();
        }
        public void WatchRestart()
        {
            sw.Restart();
        }
        public async Task WatchScopeInvokeAsync(Func<Task> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            await action();
            sw.Stop();
            _output.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");
        }
        public void WatchWriteSeconds()
        {
            _output.WriteLine($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
