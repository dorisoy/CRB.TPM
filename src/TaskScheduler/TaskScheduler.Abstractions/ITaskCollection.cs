using System.Collections.Generic;

namespace CRB.TPM.TaskScheduler.Abstractions;

/// <summary>
/// 任务集合
/// </summary>
public interface ITaskCollection : IList<ITaskDescriptor>
{
}
