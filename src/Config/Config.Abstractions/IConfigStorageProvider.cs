using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Config.Abstractions;

/// <summary>
/// 配置存储
/// </summary>
public interface IConfigStorageProvider
{
    /// <summary>
    /// 获取配置Json值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<string> GetJson(ConfigType type, string code);

    /// <summary>
    /// 保存配置Json值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    Task<bool> SaveJson(ConfigType type, string code, string json);
}
