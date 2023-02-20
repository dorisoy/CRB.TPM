
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Data.Abstractions.Queryable;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure.Repositories;

public class MdReTmnBTyteConfigRepository : RepositoryAbstract<MdReTmnBTyteConfigEntity>, IMdReTmnBTyteConfigRepository
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<PagingQueryResultModel<MdReTmnBTyteConfigEntity>> Query(MdReTmnBTyteConfigQueryDto dto)
    {
        return QueryBuilder(dto).ToPaginationResult(dto.Paging);
    }

    /// <summary>
    /// 构建查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private IQueryable<MdReTmnBTyteConfigEntity> QueryBuilder(MdReTmnBTyteConfigQueryDto dto)
    {
        var query = Find();
        return query;
    }
}