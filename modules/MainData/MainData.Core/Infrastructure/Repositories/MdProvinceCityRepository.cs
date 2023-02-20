
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdProvinceCityRepository : RepositoryAbstract<MdProvinceCityEntity>, IMdProvinceCityRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdProvinceCityEntity>> Query(MdProvinceCityQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdProvinceCityEntity> QueryBuilder(MdProvinceCityQueryDto dto)
    {
        var query = Find();
        return query;
    }
}