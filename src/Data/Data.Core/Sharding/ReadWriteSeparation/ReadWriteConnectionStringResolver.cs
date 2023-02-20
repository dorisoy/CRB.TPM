using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 用于表示读写分离链接字符串解析器
/// </summary>
public class ReadWriteConnectionStringResolver : IConnectionStringResolver
{
    private readonly ReadStrategyEnum _readStrategy;

    /// <summary>
    /// 信号量
    /// </summary>
    private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();


    /// <summary>
    /// 连接字典集
    /// </summary>
    private readonly ConcurrentDictionary<NodeType?, IReadWriteConnector> _connectors =
        new ConcurrentDictionary<NodeType?, IReadWriteConnector>();

    /// <summary>
    /// 连接工厂
    /// </summary>
    private readonly IReadWriteConnectorFactory _readWriteConnectorFactory;

    /// <summary>
    /// 实例化链接字符串解析器
    /// </summary>
    /// <param name="connectors"></param>
    /// <param name="readStrategy"></param>
    /// <param name="readWriteConnectorFactory"></param>
    public ReadWriteConnectionStringResolver(IEnumerable<IReadWriteConnector> connectors, 
        ReadStrategyEnum readStrategy, 
        IReadWriteConnectorFactory readWriteConnectorFactory)
    {
        _readStrategy = readStrategy;
        var enumerator = connectors.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentConnector = enumerator.Current;
            if (currentConnector != null)
                _connectors.TryAdd(currentConnector.NodeType, currentConnector);
        }
        _readWriteConnectorFactory = readWriteConnectorFactory;
    }

    /// <summary>
    /// 连接器是否以及包含节点类型
    /// </summary>
    /// <param name="nodeType"></param>
    /// <returns></returns>
    public bool ContainsReadWriteDataSourceName(NodeType nodeType)
    {
        return _connectors.ContainsKey(nodeType);
    }

    /// <summary>
    /// 根据节点名称获取连接字符串
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <param name="nodeName">节点名称</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetConnectionString(NodeType nodeType, string nodeName = "")
    {
        if (!_connectors.TryGetValue(nodeType, out var connector))
            throw new Exception($"没有找到读写连接器, 节点类型为:[{nodeType}]");

        return connector.GetConnectionString(nodeType,nodeName);
    }

    /// <summary>
    /// 根据节点名称获取连接字符串
    /// </summary>
    /// <param name="nodeType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<string> GetAllConnectionString(NodeType nodeType)
    {
        if (!_connectors.TryGetValue(nodeType, out var connector))
            throw new Exception($"没有找到读写连接器, 节点类型为:[{nodeType}]");

        return connector.GetAllConnectionString(nodeType);
    }

    /// <summary>
    /// 添加获取连接字符串
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="nodeName">节点名称</param>
    /// <returns></returns>
    public bool AddConnectionString(NodeType nodeType, string connectionString, string nodeName = "")
    {
        if (!_connectors.TryGetValue(nodeType, out var connector))
        {
            connector = _readWriteConnectorFactory.CreateConnector(_readStrategy, nodeType, new Node[]
                {
                    new Node(){ Name= nodeName ?? Guid.NewGuid().ToString("n"), ConnectionString = connectionString }
                });

            return _connectors.TryAdd(nodeType, connector);
        }
        else
        {
            return connector.AddConnectionString(nodeType, connectionString, nodeName);
        }
    }
}
