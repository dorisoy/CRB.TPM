using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 用于表示读写分离上下文访问器
/// </summary>
public class ReadWriteAccessor : IReadWriteAccessor
{
    private static AsyncLocal<ReadWriteContext> _readWriteContext = new AsyncLocal<ReadWriteContext>();

    /// <summary>
    /// 读写上下文
    /// </summary>
    public ReadWriteContext ReadWriteContext
    {
        get => _readWriteContext.Value;
        set => _readWriteContext.Value = value;
    }
}
