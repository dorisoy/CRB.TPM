using System.ComponentModel;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;

/// <summary>
/// 任务日志类型
/// </summary>
public enum JobLogType
{
    [Description("信息")]
    Info,
    [Description("调试")]
    Debug,
    [Description("异常")]
    Error
}
