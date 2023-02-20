using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure;

/// <summary>
/// 计划任务配置变更事件
/// </summary>
public class SchedulerConfigChangedEvent : IConfigChangeEvent<SchedulerConfig>
{
    private readonly IQuartzServer _quartzServer;

    public SchedulerConfigChangedEvent(IQuartzServer quartzServer)
    {
        _quartzServer = quartzServer;
    }

    public void OnChanged(SchedulerConfig newConfig, SchedulerConfig oldConfig)
    {
        _quartzServer.Stop();
        if (newConfig.Enabled)
            _quartzServer.Restart();
    }
}

