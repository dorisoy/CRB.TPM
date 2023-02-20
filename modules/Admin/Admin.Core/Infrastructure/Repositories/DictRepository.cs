using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Dict;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class DictRepository : RepositoryAbstract<DictEntity>, IDictRepository
{
    public Task<PagingQueryResultModel<DictEntity>> Query(DictQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<DictEntity> QueryBuilder(DictQueryDto dto)
    {
        var query = Find();
        query.WhereNotNull(dto.GroupCode, m => m.GroupCode.Equals(dto.GroupCode));
        query.WhereNotNull(dto.Name, m => m.Name.Equals(dto.Name));
        query.WhereNotNull(dto.Code, m => m.Code.Equals(dto.Code));
        return query;
    }
}