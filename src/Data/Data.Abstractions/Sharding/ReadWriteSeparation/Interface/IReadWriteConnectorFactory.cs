using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 表示用于读写连接字符串管理器工厂
    /// </summary>
    public interface IReadWriteConnectorFactory
    {
        /// <summary>
        /// 创建连接器
        /// </summary>
        /// <param name="strategy">策略</param>
        /// <param name="nodeTpye">节点类型</param>
        /// <param name="nodes">节点</param>
        /// <returns></returns>
        IReadWriteConnector CreateConnector(ReadStrategyEnum strategy, NodeType nodeTpye, Node[] nodes);
    }
}
