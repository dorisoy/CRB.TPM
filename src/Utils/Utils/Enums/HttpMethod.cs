using System.ComponentModel;

namespace CRB.TPM.Utils.Enums;

/// <summary>
/// 请求方法类型
/// </summary>
public enum HttpMethod
{
    [Description("GET")]
    Get,
    [Description("PUT")]
    Put,
    [Description("POST")]
    Post,
    [Description("DELETE")]
    Delete,
    [Description("HEAD")]
    Head,
    [Description("OPTIONS")]
    Options,
    [Description("TRACE")]
    Trace,
    [Description("PATCH")]
    Patch
}
