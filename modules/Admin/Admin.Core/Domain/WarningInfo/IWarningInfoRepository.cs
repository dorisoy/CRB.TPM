using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;

namespace CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;

/// <summary>
/// WarningInfo仓储接口
/// </summary>
public interface IWarningInfoRepository : IRepository<WarningInfoEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<WarningInfoEntity>> Query(WarningInfoQueryDto dto);
}