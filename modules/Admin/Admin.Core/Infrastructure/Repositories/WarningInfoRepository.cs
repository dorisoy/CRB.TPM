using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class WarningInfoRepository : RepositoryAbstract<WarningInfoEntity>, IWarningInfoRepository
{
    public Task<PagingQueryResultModel<WarningInfoEntity>> Query(WarningInfoQueryDto dto)
    {
        var query = QueryBuilder(dto);
        return query.ToPaginationResult(dto.Paging);
    }


    private IQueryable<WarningInfoEntity> QueryBuilder(WarningInfoQueryDto dto)
    {
        return Find()
            .Select(m => m)
            .OrderByDescending(m => m.Id);
    }

}