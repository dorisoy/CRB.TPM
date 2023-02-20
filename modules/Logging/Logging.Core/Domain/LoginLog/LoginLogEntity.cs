using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Domain.LoginLog;

/// <summary>
/// 登录日志
/// </summary>
[Table("Login_Log")]
public partial class LoginLogEntity : Entity<long>
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
    [Nullable]
    [Length(100)]
    public string UserName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Nullable]
    [Length(300)]
    public string Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [Nullable]
    [Length(20)]
    public string Phone { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    [Nullable]
    public string IP { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    [Length(500)]
    [Nullable]
    public string Error { get; set; }

    /// <summary>
    /// UA
    /// </summary>
    [Length(500)]
    [Nullable]
    public string UserAgent { get; set; }

    [NotMappingColumn]
    public string PlatformName { get; set; }

    [NotMappingColumn]
    public string LoginModeName => LoginMode.ToDescription();

}
