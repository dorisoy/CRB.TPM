using CRB.TPM.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.Role;

/// <summary>
/// 角色仓储
/// </summary>
public interface IRoleRepository : IRepository<RoleEntity>
{
    Task<IList<RoleEntity>> QueryByRouteName(string routeName);
    Task<IList<RoleEntity>> QueryByMenuId(Guid menuId);
}