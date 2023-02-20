
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccount.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MAdvertiserAccountRepository : RepositoryAbstract<MAdvertiserAccountEntity>, IMAdvertiserAccountRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MAdvertiserAccountEntity>> Query(MAdvertiserAccountQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MAdvertiserAccountEntity> QueryBuilder(MAdvertiserAccountQueryDto dto)
    {
        var query = Find();
        return query;
    }
}