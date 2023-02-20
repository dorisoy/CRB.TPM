using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Repositories;

public class GroupRepository : RepositoryAbstract<GroupEntity>, IGroupRepository
{
    public async Task<PagingQueryResultModel<GroupEntity>> Query(GroupQueryDto dto, IList<AccountEntity> accounts = null)
    {
        var query = Find();
        query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name));
        query.WhereNotNull(dto.Code, m => m.Code.Contains(dto.Code));

        if (accounts != null && accounts.Any())
            query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

        query.OrderByDescending(x => x.Id);

        return await query.Select(m => new
        {
            Creator = (accounts.Where(s => s.Id == m.CreatedBy).FirstOrDefault()).Name ?? ""
        }).ToPaginationResult(dto.Paging);
    }


    public async Task<bool> Exists(GroupEntity entity)
    {
        var query = Find(m => m.Code == entity.Code);
        query.WhereNotNull(entity.Id, m => m.Id != entity.Id);
        return await query.ToExists();
    }
}
