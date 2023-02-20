using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;

namespace CRB.TPM.Mod.Admin.Core.Domain.MenuGroup;

/// <summary>
/// 菜单组
/// </summary>
[Table("SYS_MenuGroup")]
public class MenuGroupEntity : EntityBase<Guid>
{
    /// <summary>
    /// 分组名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Length(300)]
    [Nullable]
    public string Remarks { get; set; }
}