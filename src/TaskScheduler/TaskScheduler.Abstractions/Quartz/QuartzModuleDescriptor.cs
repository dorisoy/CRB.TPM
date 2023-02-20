using System.Collections.Generic;
using CRB.TPM.Module.Abstractions;

namespace CRB.TPM.TaskScheduler.Abstractions.Quartz;

/// <summary>
/// 调度模块描述信息 (即将过时，Scheduler独立解决方案后， 将用 TaskDescriptor 替换)
/// </summary>
public class QuartzModuleDescriptor
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public ModuleDescriptor Module { get; set; }

    /// <summary>
    /// 任务列表
    /// </summary>
    public List<QuartzTaskDescriptor> Tasks { get; } = new ();

    /// <summary>
    /// 任务下拉列表
    /// </summary>
    public List<OptionResultModel> TaskSelect { get; } = new();
}
