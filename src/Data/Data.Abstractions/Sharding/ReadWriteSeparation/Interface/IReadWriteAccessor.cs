using System;
using System.Collections.Generic;
using System.Text;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 用于表示读写分离上下文访问器
    /// </summary>
    public interface IReadWriteAccessor
    {
        ReadWriteContext ReadWriteContext { get; set; }
    }
}
