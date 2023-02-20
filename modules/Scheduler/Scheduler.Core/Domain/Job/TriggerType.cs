using System.ComponentModel;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.Job;

/// <summary>
/// 触发器类型
/// </summary>
public enum TriggerType
{
    /// <summary>
    /// 简单触发器
    /// </summary>
    [Description("简单触发器")]
    Simple,
    /// <summary>
    /// CRON触发器
    /// </summary>
    [Description("CRON触发器")]
    Cron
}
