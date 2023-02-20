using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Repositories;

public class RoleRepository : RepositoryAbstract<RoleEntity>, IRoleRepository
{
    public async Task<IList<RoleEntity>> QueryByRouteName(string routeName)
    {
        return await Find()
            .InnerJoin<RoleMenuEntity>(m => m.T1.Id == m.T2.RoleId)
            .InnerJoin<MenuEntity>(m => m.T2.MenuId == m.T3.Id && m.T3.RouteName == routeName)
            .Select(m => new { m.T1 })
            .ToList();
    }

    public async Task<IList<RoleEntity>> QueryByMenuId(Guid menuId)
    {
        return await Find()
            .InnerJoin<RoleMenuEntity>(m => m.T1.Id == m.T2.RoleId)
            .InnerJoin<MenuEntity>(m => m.T2.MenuId == m.T3.Id && m.T3.Id == menuId)
            .Select(m => new { m.T1 })
            .ToList();
    }

}