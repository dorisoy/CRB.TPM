using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 用于表示读写分离作用范围
    /// </summary>
    public class ReadWriteScope : IDisposable
    {
        public IReadWriteAccessor ReadWriteAccessor { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="readWriteAccessor"></param>
        public ReadWriteScope(IReadWriteAccessor readWriteAccessor)
        {
            ReadWriteAccessor = readWriteAccessor;
        }

        /// <summary>
        /// 回收
        /// </summary>
        public void Dispose()
        {
            ReadWriteAccessor.ReadWriteContext = null;
        }
    }
}
