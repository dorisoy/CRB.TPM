
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdSaleLine.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdSaleLineRepository : RepositoryAbstract<MdSaleLineEntity>, IMdSaleLineRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdSaleLineEntity>> Query(MdSaleLineQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdSaleLineEntity> QueryBuilder(MdSaleLineQueryDto dto)
    {
        var query = Find();
        return query;
    }
}