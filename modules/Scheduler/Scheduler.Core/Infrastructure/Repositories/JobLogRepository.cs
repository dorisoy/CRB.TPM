using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Repositories;

public class JobLogRepository : RepositoryAbstract<JobLogEntity>, IJobLogRepository
{
    public async Task<PagingQueryResultModel<JobLogEntity>> Query(JobLogQueryDto dto)
    {
        var paging = dto.Paging;

        var query = Find();
        query.Where(m => m.JobId == dto.JobId);
        query.WhereNotNull(dto.StartDate, m => m.CreatedTime >= dto.StartDate.Value);
        query.WhereNotNull(dto.EndDate, m => m.CreatedTime < dto.EndDate.Value.AddDays(1));

        if (!paging.OrderBy.Any())
        {
            query.OrderByDescending(m => m.Id);
        }

        return await query.ToPaginationResult(paging); 
    }
}
