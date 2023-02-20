using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Redis.Tests.XUnitExtensions
{
    public static class LoggerExtensions
    {
        public static async Task WatchScopeInvokeAsync(this ILogger logger, Func<Task> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            await action();
            sw.Stop();
            logger.LogInformation($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");
        }

        public static void WatchScopeInvoke(this ILogger logger, Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            logger.LogInformation($"耗时: {sw.Elapsed.ToString(@"hh\:mm\:s\.fff")}");
        }
    }
}
