using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;
using CRB.TPM.Utils.Annotations;
using System;

namespace CRB.TPM.Mod.Admin.Core.Application.LoginLog.Dto;

[ObjectMap(typeof(LoginLogEntity))]
public class LoginLogAddDto
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid? AccountId { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public int Platform { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public LoginMode LoginMode { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// UA
    /// </summary>
    public string UserAgent { get; set; }
}
