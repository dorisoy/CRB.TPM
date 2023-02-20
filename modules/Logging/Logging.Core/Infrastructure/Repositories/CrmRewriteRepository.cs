
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;

namespace CRB.TPM.Mod.Logging.Core.Infrastructure.Repositories;

public class CrmRewriteRepository : RepositoryAbstract<CrmRewriteEntity>, ICrmRewriteRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<CrmRewriteEntity>> Query(CrmRewriteQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<CrmRewriteEntity> QueryBuilder(CrmRewriteQueryDto dto)
    {
        var query = Find();
        return query;
    }
}