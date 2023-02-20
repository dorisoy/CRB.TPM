using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;


namespace CRB.TPM.Mod.Admin.Core.Application.Account.Vo;

/// <summary>
/// 账户同步模型
/// </summary>
//[ObjectMap(typeof(AccountEntity))]
public class AccountSyncVo
{
    public Guid Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 加密前的新密码(为空表示不修改密码)
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    /// 绑定角色列表，如果为Null表示不更新角色
    /// </summary>
    public IList<Guid> Roles { get; set; } = new List<Guid>();
}



public class AccountSelectVo
{
    public Guid Value { get; set; }
    public string Lable { get; set; }
}