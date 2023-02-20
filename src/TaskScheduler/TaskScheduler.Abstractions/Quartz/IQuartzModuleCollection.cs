using System.Collections.Generic;

namespace CRB.TPM.TaskScheduler.Abstractions.Quartz;

/// <summary>
/// 模块任务集合
/// </summary>
public interface IQuartzModuleCollection : IList<QuartzModuleDescriptor>
{
}
