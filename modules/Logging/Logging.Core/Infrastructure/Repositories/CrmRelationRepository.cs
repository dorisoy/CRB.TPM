
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

namespace CRB.TPM.Mod.Logging.Core.Infrastructure.Repositories;

public class CrmRelationRepository : RepositoryAbstract<CrmRelationEntity>, ICrmRelationRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<CrmRelationEntity>> Query(CrmRelationQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<CrmRelationEntity> QueryBuilder(CrmRelationQueryDto dto)
    {
        var query = Find();
        return query;
    }
}