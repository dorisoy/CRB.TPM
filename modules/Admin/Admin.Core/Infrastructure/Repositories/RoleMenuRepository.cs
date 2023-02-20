using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using StackExchange.Redis;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class RoleMenuRepository : RepositoryAbstract<RoleMenuEntity>, IRoleMenuRepository
{
    /// <summary>
    /// 根据角色查询角色菜单
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<IList<RoleMenuEntity>> QueryByRoleId(Guid roleId)
    {
        return await Find(e => e.RoleId == roleId).ToList();
    }

    public async Task<bool> DeleteByMenuId(Guid menuId, IUnitOfWork uow)
    {
        return await Find(e => e.MenuId == menuId).UseUow(uow).ToDelete();
    }

    public async Task<bool> ExistsWidthMenu(Guid menuId)
    {
        return await Find(e => e.MenuId == menuId).ToExists();
    }

    public async Task<bool> DeleteByRoleId(Guid roleId, IUnitOfWork uow)
    {
        return await Find(e => e.RoleId == roleId).UseUow(uow).ToDelete();
    }

    public async Task<IList<RoleMenuEntity>> QueryByRouteName(string routeName)
    {
        return await Find()
            .InnerJoin<RoleEntity>(m => m.T1.RoleId == m.T2.Id)
            .InnerJoin<MenuEntity>(m => m.T1.MenuId == m.T3.Id && m.T3.RouteName == routeName)
            .Select(m => new { RoleId = m.T2.Id })
            .ToList();
    }
}
