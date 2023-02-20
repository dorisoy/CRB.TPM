
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDetail.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MTerminalDetailRepository : RepositoryAbstract<MTerminalDetailEntity>, IMTerminalDetailRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MTerminalDetailEntity>> Query(MTerminalDetailQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MTerminalDetailEntity> QueryBuilder(MTerminalDetailQueryDto dto)
    {
        var query = Find();
        return query;
    }
}