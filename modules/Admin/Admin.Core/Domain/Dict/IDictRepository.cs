using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;

namespace CRB.TPM.Mod.Admin.Core.Domain.Dict;

/// <summary>
/// 数据字典仓储
/// </summary>
public interface IDictRepository : IRepository<DictEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<DictEntity>> Query(DictQueryDto dto);
}