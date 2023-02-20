using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;

public interface IJobLogRepository : IRepository<JobLogEntity>
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<JobLogEntity>> Query(JobLogQueryDto dto);
}
