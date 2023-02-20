using CRB.TPM.TaskScheduler.Abstractions;
using CRB.TPM.Utils.Abstracts;

namespace CRB.TPM.TaskScheduler.Core
{
    /// <summary>
    /// 表示任务集合
    /// </summary>
    internal class TaskCollection : CollectionAbstract<ITaskDescriptor>, ITaskCollection
    {
    }
}
