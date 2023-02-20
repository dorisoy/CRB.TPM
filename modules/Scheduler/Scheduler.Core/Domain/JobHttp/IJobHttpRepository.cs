using CRB.TPM.Data.Abstractions;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;

public interface IJobHttpRepository : IRepository<JobHttpEntity>
{
    /// <summary>
    /// 根据任务编号查询
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    Task<JobHttpEntity> GetByJob(Guid jobId);
}
