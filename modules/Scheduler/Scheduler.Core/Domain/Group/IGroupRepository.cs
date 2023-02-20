using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.Group;

/// <summary>
/// 任务组仓储
/// </summary>
public interface IGroupRepository : IRepository<GroupEntity>
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<GroupEntity>> Query(GroupQueryDto dto, IList<AccountEntity> accounts = null);

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> Exists(GroupEntity entity);
}
