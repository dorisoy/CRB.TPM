
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MAdvertiserAccountAddressRepository : RepositoryAbstract<MAdvertiserAccountAddressEntity>, IMAdvertiserAccountAddressRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MAdvertiserAccountAddressEntity>> Query(MAdvertiserAccountAddressQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MAdvertiserAccountAddressEntity> QueryBuilder(MAdvertiserAccountAddressQueryDto dto)
    {
        var query = Find();
        return query;
    }
}