using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using CRB.TPM.Utils.App;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.TaskScheduler.Core.Quartz;

/// <summary>
///  处理应用程序关闭时，停止Quartz作业
/// </summary>
public class QuartzAppShutdownHandler : IAppShutdownHandler
{
    private readonly IQuartzServer _server;
    private readonly ILogger _logger;

    public QuartzAppShutdownHandler(IQuartzServer server, ILogger<QuartzAppShutdownHandler> logger)
    {
        _server = server;
        _logger = logger;
    }

    public async Task Handle()
    {
        if (_server != null)
        {
            await _server.Stop();

            _logger.LogDebug("quartz server stop");
        }
    }
}
