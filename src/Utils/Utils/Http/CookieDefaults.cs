using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Http;

/// <summary>
/// 表示与cookie相关的默认值
/// </summary>
public class CookieDefaults
{
    public static string Prefix => "CRB.TPM";

    /// <summary>
    /// 获取反伪造的cookie名称
    /// </summary>
    public static string AntiforgeryCookie => ".Antiforgery";

}
