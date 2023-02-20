
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdCityDistrictRepository : RepositoryAbstract<MdCityDistrictEntity>, IMdCityDistrictRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdCityDistrictEntity>> Query(MdCityDistrictQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdCityDistrictEntity> QueryBuilder(MdCityDistrictQueryDto dto)
    {
        var query = Find();
        return query;
    }
}