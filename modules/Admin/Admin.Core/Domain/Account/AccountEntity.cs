using System;
using System.ComponentModel;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Domain.Account;

/// <summary>
/// 账户信息
/// </summary>
[EnableEntityAllEvent]
[Table("SYS_Account")]
public partial class AccountEntity : EntityBaseSoftDelete<Guid>, ITenant
{
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public AccountType Type { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [IgnoreOnExcelExport]
    public string Password { get; set; }

    /// <summary>
    /// 用户姓名或者企业名称，具体是什么与业务有关
    /// </summary>
    [Length(250)]
    [Description("姓名")]
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [Length(20)]
    [Nullable]
    [Description("手机号")]
    public string Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Length(300)]
    [Nullable]
    [Description("邮箱")]
    public string Email { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Description("状态")]
    public AccountStatus Status { get; set; }

    /// <summary>
    /// 激活时间
    /// </summary>
    public DateTime ActivatedTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 是否锁定，锁定后不允许在账户管理中修改
    /// </summary>
    public bool IsLock { get; set; }


    /// <summary>
    /// 组织Id，仅代表用户组织，不用于数据权限
    /// </summary>
    [Nullable]
    public Guid OrgId { get; set; }

    /// <summary>
    /// UserBP
    /// </summary>
    [Nullable]
    public string UserBP { get; set; }

    /// <summary>
    /// LDAP账户名
    /// </summary>
    [Nullable]
    public string LDAPName { get; set; }
}

/// <summary>
/// 账户状态
/// </summary>
public enum AccountStatus
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,
    /// <summary>
    /// 已注册
    /// </summary>
    [Description("注册")]
    Register,
    /// <summary>
    /// 已激活
    /// </summary>
    [Description("激活")]
    Active,
    /// <summary>
    /// 已禁用
    /// </summary>
    [Description("禁用")]
    Disabled,
    /// <summary>
    /// 已注销
    /// </summary>
    [Description("注销")]
    Logout
}