using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.PS.Core.Application.Post.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Infrastructure.Repositories;

public class PostRepository : RepositoryAbstract<PostEntity>, IPostRepository
{
    public async Task<PagingQueryResultModel<PostEntity>> Query(PostQueryDto dto, IList<AccountEntity> accounts)
    {
        var query = Find();
        query.WhereNotNull(dto.Name, m => m.Name.Contains(dto.Name) || m.ShortName.Contains(dto.Name));

        if (accounts != null && accounts.Any())
            query.Where(m => accounts.Select(s => s.Id).Contains(m.CreatedBy));

        //var joinQuery = query.LeftJoin<PositionEntity>(m => m.T1.PositionId == m.T2.Id)
        //    .LeftJoin<AccountEntity>(m => m.T1.CreatedBy == m.T3.Id);

        var joinQuery = query.LeftJoin<PositionEntity>(m => m.T1.PositionId == m.T2.Id);

        if (!dto.Paging.OrderBy.Any())
        {
            joinQuery.OrderByDescending(m => m.T1.Id);
        }
        joinQuery.Select(m => new { m.T1, PositionName = m.T2.Name });

        return await joinQuery.ToPaginationResult(dto.Paging);
    }

    public async Task<bool> ExistsPosition(Guid positionId)
    {
        return await Find(m => m.PositionId == positionId).ToExists();
    }

    public async Task<bool> ExistsName(string name, Guid? id = null)
    {
        var query = Find(m => m.Name == name);
        query.WhereNotNull(id, m => m.Id != id.Value);
        return await query.ToExists();
    }
}
