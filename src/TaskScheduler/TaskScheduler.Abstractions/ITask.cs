using System.Threading.Tasks;
using Quartz;

namespace CRB.TPM.TaskScheduler.Abstractions;

/// <summary>
/// 用于表示任务接口
/// </summary>
public interface ITask : IJob
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
   Task Execute(TaskExecutionContext context);
}
