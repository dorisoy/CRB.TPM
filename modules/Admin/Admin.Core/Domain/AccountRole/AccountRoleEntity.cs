using System;
using System.ComponentModel;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Domain.AccountRole;

/// <summary>
/// 账户角色
/// </summary>
[Table("SYS_Account_Role")]
public class AccountRoleEntity : Entity<Guid>
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 角色编号
    /// </summary>
    public Guid RoleId { get; set; }
}

