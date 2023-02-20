
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdStreetVillageRepository : RepositoryAbstract<MdStreetVillageEntity>, IMdStreetVillageRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdStreetVillageEntity>> Query(MdStreetVillageQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdStreetVillageEntity> QueryBuilder(MdStreetVillageQueryDto dto)
    {
        var query = Find();
        return query;
    }
}