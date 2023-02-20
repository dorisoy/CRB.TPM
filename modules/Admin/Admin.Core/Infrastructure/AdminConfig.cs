using CRB.TPM.Config.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure;

/// <summary>
/// 权限管理（身份认证和授权配置）
/// </summary>
public class AdminConfig : IConfig
{
    /// <summary>
    /// 账户默认密码(新增账户或者重置密码时使用)
    /// </summary>
    public string DefaultPassword { get; set; } = "123456";

    /// <summary>
    /// 启用登录日志
    /// </summary>
    public bool LoginLog { get; set; }
}


