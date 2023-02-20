using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.PS.Core.Application.Position.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class PositionRepository : RepositoryAbstract<PositionEntity>, IPositionRepository
{

    public async Task<PagingQueryResultModel<PositionEntity>> Query(PositionQueryDto dto, IList<AccountEntity> accounts)
    {
        var query = Find();
        query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name));
        query.WhereNotNull(dto.Code, m => m.Code == dto.Code);


        if (accounts != null && accounts.Any())
            query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

       // var joinQuery = query.LeftJoin<AccountEntity>(m => m.T1.CreatedBy == m.T2.Id);

        if (!dto.Paging.OrderBy.Any())
        {
            query.OrderByDescending(m => m.Id);
        }

        //query.Select(m => new { m.T1, Creator = m.T2.Name });

        return await query.ToPaginationResult(dto.Paging);
    }

    public async Task<bool> ExistsName(string name, Guid? id = null)
    {
        var query = Find(m => m.Name == name);
        query.WhereNotNull(id, m => m.Id != id.Value);
        return await query.ToExists();
    }

    public async Task<bool> ExistsCode(string code, Guid? id = null)
    {
        var query = Find(m => m.Code == code);
        query.WhereNotNull(id, m => m.Id != id.Value);
        return await query.ToExists();
    }
}
