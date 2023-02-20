using System.ComponentModel;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;

/// <summary>
/// 认证方式
/// </summary>
public enum AuthType
{
    /// <summary>
    /// None
    /// </summary>
    [Description("None")]
    None,

    /// <summary>
    /// Jwt
    /// </summary>
    [Description("Jwt")]
    Jwt
}
