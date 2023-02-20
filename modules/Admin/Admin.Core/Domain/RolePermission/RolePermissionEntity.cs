using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;

namespace CRB.TPM.Mod.Admin.Core.Domain.RolePermission;

/// <summary>
/// 角色权限绑定关系
/// </summary>
[Table("SYS_Role_Permission")]
public class RolePermissionEntity : Entity<Guid>
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public int Platform { get; set; }

    /// <summary>
    /// 角色编号
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 菜单组编号
    /// </summary>
    public Guid MenuGroupId { get; set; }

    /// <summary>
    /// 菜单编号
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; }
}