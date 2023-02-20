
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MProductMarketingPropertyRepository : RepositoryAbstract<MProductMarketingPropertyEntity>, IMProductMarketingPropertyRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MProductMarketingPropertyEntity>> Query(MProductMarketingPropertyQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MProductMarketingPropertyEntity> QueryBuilder(MProductMarketingPropertyQueryDto dto)
    {
        var query = Find();
        return query;
    }
}