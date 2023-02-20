using CRB.TPM.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.RolePermission;

/// <summary>
/// 角色权限绑定关系仓储
/// </summary>
public interface IRolePermissionRepository : IRepository<RolePermissionEntity>
{
    Task<IList<string>> QueryByRole(Guid roleId, int platform);
    Task<IList<string>> QueryByAccount(Guid accountId, int platform);
    Task<bool> DeleteByRole(Guid roleId, int platform, IUnitOfWork uow = null);
}