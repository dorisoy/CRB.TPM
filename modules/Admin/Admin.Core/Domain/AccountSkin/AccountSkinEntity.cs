using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.Admin.Core.Domain.AccountSkin;

/// <summary>
/// 账户配置
/// </summary>
[Table("SYS_AccountSkin")]
public class AccountSkinEntity  : Entity<Guid>
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 皮肤名称
    /// </summary>
    [Nullable]
    public string Name { get; set; }

    /// <summary>
    /// 皮肤编码
    /// </summary>
    [Nullable]
    public string Code { get; set; }

    /// <summary>
    /// 皮肤主题
    /// </summary>
    [Nullable]
    public string Theme { get; set; }

    /// <summary>
    /// 皮肤尺寸
    /// </summary>
    public string Size { get; set; }
}