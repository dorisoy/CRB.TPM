using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;

namespace CRB.TPM.Mod.Admin.Core.Domain.Role;

/// <summary>
/// 角色
/// </summary>
[Table("SYS_Role")]
public partial class RoleEntity : EntityBaseSoftDelete
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Length(300)]
    public string Remarks { get; set; }

    /// <summary>
    /// 锁定的，不允许修改
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// 绑定的菜单分组编号
    /// </summary>
    public Guid MenuGroupId { get; set; }

    /// <summary>
    /// CRMCode
    /// </summary>
    [Nullable]
    public string CRMCode { get; set; }

    /// <summary>
    /// 组织Id
    /// </summary>
    [Nullable]
    public string OrgId { get; set; }

    /// <summary>
    /// 组织名称
    /// </summary>
    [Nullable]
    public string OrgName { get; set; }
}