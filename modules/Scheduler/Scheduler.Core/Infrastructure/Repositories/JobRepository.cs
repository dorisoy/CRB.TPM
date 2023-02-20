using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Repositories;

public class JobRepository : RepositoryAbstract<JobEntity>, IJobRepository
{
    public async Task<PagingQueryResultModel<JobEntity>> Query(JobQueryDto dto, IList<AccountEntity> accounts = null)
    {
        var paging = dto.Paging;

        var query = Find();
        query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name));

        if (accounts != null && accounts.Any())
            query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

        if (!paging.OrderBy.Any())
        {
            query.OrderByDescending(x => x.Id);
        }

        return await query.Select(m => new
        {
            Creator = (accounts.Where(s => s.Id == m.CreatedBy).FirstOrDefault()).Name ?? ""
        }).ToPaginationResult(dto.Paging);
    }

    public async Task<bool> Exists(JobEntity entity)
    {
        var query = Find(m => m.Code == entity.Code && m.Group == entity.Group);
        query.WhereNotNull(entity.Id, m => m.Id != entity.Id);

        return await query.ToExists();
    }

    public async Task<bool> ExistsByGroup(string group)
    {
        return await Find(m => m.Group == group).ToExists();
    }

    public async Task<bool> UpdateStatus(string group, string code, JobStatus status)
    {

        return await Find(m => m.Group == group && m.Code == code).ToUpdate(m => new JobEntity { Status = status });
    }

    public async Task<bool> UpdateStatus(Guid id, JobStatus status, IUnitOfWork uow = null)
    {
        return await Find(m => m.Id == id).UseUow(uow).ToUpdate(m => new JobEntity { Status = status });
    }

    public async Task<bool> HasStop(string @group, string code)
    {
        return await Find(m => m.Group == group && m.Code == code && m.Status == JobStatus.Stop).ToExists();
    }
}
