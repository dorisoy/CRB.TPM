using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Http;

/// <summary>
/// 表示与HTTP功能相关的默认值
/// </summary>
public class HttpDefaults
{
    /// <summary>
    /// HTTP_CLUSTER_HTTPS 头
    /// </summary>
    public static string HttpClusterHttpsHeader => "HTTP_CLUSTER_HTTPS";

    /// <summary>
    /// HTTP_X_FORWARDED_PROTO 头
    /// </summary>
    public static string HttpXForwardedProtoHeader => "X-Forwarded-Proto";

    /// <summary>
    /// X-FORWARDED-FOR 头
    /// </summary>
    public static string XForwardedForHeader => "X-FORWARDED-FOR";

    /// <summary>
    /// AngularJS 配置防伪服务查找名为X-XSRF-TOKEN的请求头
    /// AngularJS通过约定来解决CSRF。如果服务器发送带有名称为XSRF-TOKEN的Cookie ，则Angular的$http服务将向该服务器发送的请求将该Cookie的值添加到请求头。这个过程是自动的，您不需要明确设置请求头。请求头的名称是X-XSRF-TOKEN，服务器会检测该请求头并验证其内容。
    /// </summary>
    public static string XXSRFTOKENForHeader => "X-XSRF-TOKEN";

    /// <summary>
    /// JavaScript防伪请求头
    /// </summary>
    public static string XCSRFTOKENNForHeader => "X-CSRF-TOKEN";

}
