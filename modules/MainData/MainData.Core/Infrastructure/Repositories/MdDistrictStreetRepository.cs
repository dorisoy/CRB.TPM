
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdDistrictStreetRepository : RepositoryAbstract<MdDistrictStreetEntity>, IMdDistrictStreetRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdDistrictStreetEntity>> Query(MdDistrictStreetQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdDistrictStreetEntity> QueryBuilder(MdDistrictStreetQueryDto dto)
    {
        var query = Find();
        return query;
    }
}