using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 表示用于读写连接字符串管理器工厂
/// </summary>
public class ReadWriteConnectorFactory : IReadWriteConnectorFactory
{
    /// <summary>
    /// 创建连接器
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="nodeType"></param>
    /// <param name="nodes"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IReadWriteConnector CreateConnector(ReadStrategyEnum strategy, NodeType nodeType, Node[] nodes)
    {

        if (strategy == ReadStrategyEnum.Loop)
        {
            return new ReadWriteLoopConnector(nodeType, nodes);
        }
        else if (strategy == ReadStrategyEnum.Random)
        {
            return new ReadWriteRandomConnector(nodeType, nodes);
        }
        else
        {
            throw new Exception($"未知的读写策略:[{strategy}]");
        }
    }
}
