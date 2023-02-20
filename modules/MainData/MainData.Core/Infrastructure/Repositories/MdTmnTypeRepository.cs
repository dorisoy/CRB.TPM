
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdTmnTypeRepository : RepositoryAbstract<MdTmnTypeEntity>, IMdTmnTypeRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdTmnTypeEntity>> Query(MdTmnTypeQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdTmnTypeEntity> QueryBuilder(MdTmnTypeQueryDto dto)
    {
        var query = Find();
        return query;
    }
}