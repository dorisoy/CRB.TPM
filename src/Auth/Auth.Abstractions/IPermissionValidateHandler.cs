using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Utils.Enums;

namespace CRB.TPM.Auth.Abstractions;

/// <summary>
/// 权限验证处理接口
/// </summary>
public interface IPermissionValidateHandler
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <returns></returns>
    Task<bool> Validate(IDictionary<string, object> routeValues, HttpMethod httpMethod);
}