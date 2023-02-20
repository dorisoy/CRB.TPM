
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdHeightConfRepository : RepositoryAbstract<MdHeightConfEntity>, IMdHeightConfRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdHeightConfEntity>> Query(MdHeightConfQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdHeightConfEntity> QueryBuilder(MdHeightConfQueryDto dto)
    {
        var query = Find();
        return query;
    }
}