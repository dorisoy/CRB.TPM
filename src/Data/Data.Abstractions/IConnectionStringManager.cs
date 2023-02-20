using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions
{
    /// <summary>
    /// 用于表示数据库连接字符串管理器
    /// </summary>
    public interface IConnectionStringManager
    {
        IEnumerable<string> GetAllConnectionString(NodeType nodeType);
    }
}
