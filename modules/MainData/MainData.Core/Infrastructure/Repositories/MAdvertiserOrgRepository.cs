
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MAdvertiserOrgRepository : RepositoryAbstract<MAdvertiserOrgEntity>, IMAdvertiserOrgRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MAdvertiserOrgEntity>> Query(MAdvertiserOrgQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MAdvertiserOrgEntity> QueryBuilder(MAdvertiserOrgQueryDto dto)
    {
        var query = Find();
        return query;
    }
}