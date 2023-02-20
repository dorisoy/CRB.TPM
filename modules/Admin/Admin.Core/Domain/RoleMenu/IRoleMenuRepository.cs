using CRB.TPM.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;

/// <summary>
/// 角色菜单绑定关系仓储
/// </summary>
public interface IRoleMenuRepository : IRepository<RoleMenuEntity>
{
    Task<IList<RoleMenuEntity>> QueryByRoleId(Guid roleId);
    Task<bool> DeleteByMenuId(Guid menuId, IUnitOfWork uow);
    Task<bool> ExistsWidthMenu(Guid menuId);
    Task<bool> DeleteByRoleId(Guid roleId, IUnitOfWork uow);
    Task<IList<RoleMenuEntity>> QueryByRouteName(string routeName);
}