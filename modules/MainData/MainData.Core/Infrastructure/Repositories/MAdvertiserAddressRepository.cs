
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MAdvertiserAddressRepository : RepositoryAbstract<MAdvertiserAddressEntity>, IMAdvertiserAddressRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MAdvertiserAddressEntity>> Query(MAdvertiserAddressQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MAdvertiserAddressEntity> QueryBuilder(MAdvertiserAddressQueryDto dto)
    {
        var query = Find();
        return query;
    }
}