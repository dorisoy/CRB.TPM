using System;
using System.ComponentModel;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Domain.AccountRoleOrg;


/// <summary>
/// 用户角色组织，表示一个用户多个角色 对应不同数据权限
/// </summary>
[Table("SYS_Account_Role_Org")]
public class AccountRoleOrgEntity : Entity<Guid>
{ 
    public Guid Account_RoleId { get; set; }
    public string OrgId { get; set; }
}