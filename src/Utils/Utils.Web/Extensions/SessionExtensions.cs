using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace RB.TPM.Utils.Web.Extensions;

/// <summary>
/// 用于表示Session扩展
/// </summary>
public static class SessionExtensions
{
    /// <summary>
    /// 设置session值
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    /// <param name="session">Session</param>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize<T>(value));
    }

    /// <summary>
    /// 获取session值
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    /// <param name="session">Session</param>
    /// <param name="key">Key</param>
    /// <returns>Value</returns>
    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>(value);
    }
}
