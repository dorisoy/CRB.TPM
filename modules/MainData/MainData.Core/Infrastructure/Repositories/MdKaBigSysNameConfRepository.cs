
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdKaBigSysNameConfRepository : RepositoryAbstract<MdKaBigSysNameConfEntity>, IMdKaBigSysNameConfRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdKaBigSysNameConfEntity>> Query(MdKaBigSysNameConfQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdKaBigSysNameConfEntity> QueryBuilder(MdKaBigSysNameConfQueryDto dto)
    {
        var query = Find();
        return query;
    }
}