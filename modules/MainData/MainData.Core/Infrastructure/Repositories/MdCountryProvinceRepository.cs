
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdCountryProvinceRepository : RepositoryAbstract<MdCountryProvinceEntity>, IMdCountryProvinceRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdCountryProvinceEntity>> Query(MdCountryProvinceQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdCountryProvinceEntity> QueryBuilder(MdCountryProvinceQueryDto dto)
    {
        var query = Find();
        return query;
    }
}