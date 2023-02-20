using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Dict;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.AccountRoleOrg;

/// <summary>
/// 账户角色组织仓储
/// </summary>
public interface IAccountRoleOrgRepository : IRepository<AccountRoleOrgEntity>
{
    /// <summary>
    /// 移除账户角色组织关系
    /// </summary>
    /// <param name="accountRoleId"></param>
    /// <param name="uow"></param>
    /// <returns></returns>
    Task<bool> DeleteOrgByAccountRole(Guid accountRoleId, IUnitOfWork uow);

    /// <summary>
    /// 移除账户角色组织关系
    /// </summary>
    /// <param name="accountRoleIds"></param>
    /// <param name="uow"></param>
    /// <returns></returns>
    Task<bool> DeleteOrgByAccountRole(IList<Guid> accountRoleIds, IUnitOfWork uow);

    /// <summary>
    /// 根据账户角色获取账户角色组织关系
    /// </summary>
    /// <param name="accountRoleIds"></param>
    /// <returns></returns>
    Task<IList<AccountRoleOrgEntity>> QueryByAccountRole(IList<Guid> accountRoleIds);
}
