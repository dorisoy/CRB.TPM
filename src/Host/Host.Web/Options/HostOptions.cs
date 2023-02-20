using System.Collections.Generic;

namespace CRB.TPM.Host.Web.Options;

/// <summary>
/// 宿主配置项
/// </summary>
public class HostOptions
{
    /// <summary>
    /// 绑定的地址(默认：http://*:5000)
    /// </summary>
    public string Urls { get; set; }

    /// <summary>
    /// 基础路径
    /// </summary>
    public string Base { get; set; }

    /// <summary>
    /// 是否开启Swagger功能
    /// </summary>
    public bool Swagger { get; set; }

    /// <summary>
    /// 指定跨域访问时预检请求的有效期，单位秒，默认30分钟
    /// </summary>
    public int PreflightMaxAge { get; set; }

    /// <summary>
    /// 是否开启跨域（注意：生成环境下禁止开启）
    /// </summary>
    public bool NoCorsPolicy { get; set; }

    /// <summary>
    /// 是否启用代理
    /// </summary>
    public bool Proxy { get; set; }

    /// <summary>
    /// 开放的wwwroot下的目录列表
    /// </summary>
    public List<string> OpenDirs { get; set; } = new() { "web" };

    /// <summary>
    /// 默认目录
    /// </summary>
    public string DefaultDir { get; set; } = "web";

    ///// <summary>
    ///// 是否强制所有页面使用SSL
    ///// </summary>
    //public bool ForceSslForAllPages { get; set; }

    ///// <summary>
    ///// 是否允许在标题中使用非ascii字符
    ///// </summary>
    //public bool AllowNonAsciiCharactersInHeaders { get; set; }

    ///// <summary>
    ///// 获取或设置自定义转发的http头 (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
    ///// </summary>
    //public string ForwardedHttpHeader { get; set; }

    ///// <summary>
    ///// 是否使用 HTTP_CLUSTER_HTTPS
    ///// </summary>
    //public bool UseHttpClusterHttps { get; set; }

    ///// <summary>
    ///// 是否使用 HTTP_X_FORWARDED_PROTO
    ///// 注：（ X-Forwarded-Proto（XFP）报头是用于识别协议HTTP或HTTPS的，即用户客户端实际连接到代理或负载均衡的标准报头。
    ///// 后端的服务器如果要确定客户端和负载平衡器之间使用的协议，可以使用X-Forwarded-Proto请求标头）
    ///// </summary>
    //public bool UseHttpXForwardedProto { get; set; }
}