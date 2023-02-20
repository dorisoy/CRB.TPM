using System;
using System.IO;
using CRB.TPM.Config.Abstractions;

namespace CRB.TPM.Module.Abstractions.Options;

/// <summary>
/// 通用配置
/// </summary>
public class CommonOptions
{
    /// <summary>
    /// 临时文件目录，默认应用程序根目录中的Temp目录
    /// </summary>
    public string DefaultTempDir => Path.Combine(AppContext.BaseDirectory, "Temp");

    /// <summary>
    /// 临时目录，默认是应用程序根目录下的Temp目录
    /// </summary>
    public string TempDir { get; set; }

    /// <summary>
    /// 默认语言
    /// </summary>
    public string Lang { get; set; }

    /// <summary>
    /// 数据库配置（模块数据配置）
    /// </summary>
    public ModuleDbOptions Db { get; set; }

    /// <summary>
    /// 是否启用全局客户端模式（使用该模式有别于基于工作单元的仓储模式，可以更加灵活使用数据提供上下文）
    /// </summary>
    public bool UseClientMode { get; set; } = true;

    /// <summary>
    /// 用于表示配置提供器使用什么样的方式存储系统配置
    /// </summary>
    public ConfigProvider ConfigProvider { get; set; } =  ConfigProvider.DB;

    //2022-11-09 追加

    /// <summary>
    /// 是否强制所有页面使用SSL
    /// </summary>
    public bool ForceSslForAllPages { get; set; }

    /// <summary>
    /// 是否允许在标题中使用非ascii字符
    /// </summary>
    public bool AllowNonAsciiCharactersInHeaders { get; set; }

    /// <summary>
    /// 获取或设置自定义转发的http头 (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
    /// </summary>
    public string ForwardedHttpHeader { get; set; }

    /// <summary>
    /// 是否使用 HTTP_CLUSTER_HTTPS
    /// </summary>
    public bool UseHttpClusterHttps { get; set; }

    /// <summary>
    /// 是否使用 HTTP_X_FORWARDED_PROTO
    /// 注：（ X-Forwarded-Proto（XFP）报头是用于识别协议HTTP或HTTPS的，即用户客户端实际连接到代理或负载均衡的标准报头。
    /// 后端的服务器如果要确定客户端和负载平衡器之间使用的协议，可以使用X-Forwarded-Proto请求标头）
    /// </summary>
    public bool UseHttpXForwardedProto { get; set; }
}
