using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;

namespace CRB.TPM.Mod.Admin.Core.Domain.RoleButton;

/// <summary>
/// 角色按钮绑定关系
/// </summary>
[Table("SYS_Role_Button")]
public class RoleButtonEntity : Entity<Guid>
{
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
    /// 按钮编码
    /// </summary>
    public string ButtonCode { get; set; }
}