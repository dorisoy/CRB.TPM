using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using System;

namespace CRB.TPM.Mod.Admin.Core.Domain.RoleMenu;

/// <summary>
/// 角色菜单绑定信息
/// </summary>
[Table("SYS_Role_Menu")]
public class RoleMenuEntity : Entity<Guid>
{
    /// <summary>
    /// 角色编号
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 菜单分组编号
    /// </summary>
    public Guid MenuGroupId { get; set; }

    /// <summary>
    /// 菜单编号
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType MenuType { get; set; }
}