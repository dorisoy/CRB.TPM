using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Position.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using System;

namespace CRB.TPM.Mod.PS.Core.Domain.Position;

/// <summary>
/// 职位仓储
/// </summary>
public interface IPositionRepository : IRepository<PositionEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<PositionEntity>> Query(PositionQueryDto dto, IList<AccountEntity> accounts);

    /// <summary>
    /// 名称是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsName(string name, Guid? id = null);

    /// <summary>
    /// 编码是否存在
    /// </summary>
    /// <param name="code"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsCode(string code, Guid? id = null);
}
