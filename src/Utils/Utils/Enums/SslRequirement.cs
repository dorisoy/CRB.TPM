using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Utils.Enums;

/// <summary>
/// SSL 枚举
/// </summary>
public enum SslRequirement
{
    /// <summary>
    /// 保护
    /// </summary>
    Yes,
    /// <summary>
    /// 不保护
    /// </summary>
    No,
    /// <summary>
    /// 按要求
    /// </summary>
    NoMatter,
}
