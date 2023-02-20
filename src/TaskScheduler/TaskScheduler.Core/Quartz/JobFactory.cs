using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.TaskScheduler.Core.Quartz;

/// <summary>
/// 创建一个作业工厂
/// </summary>
public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _container;

    public JobFactory(IServiceProvider container)
    {
        _container = container;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var job = _container.GetService(bundle.JobDetail.JobType) as IJob;
        return job;
    }

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
}
