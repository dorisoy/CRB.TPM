using System;
using System.Collections.Generic;
using System.Text;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 用于表示读写分离管理器
    /// </summary>
    public interface IReadWriteManager
    {
        ReadWriteContext GetCurrent();

        ReadWriteScope CreateScope();
    }
}
