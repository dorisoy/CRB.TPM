using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Options;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 表示用于读写连接字符串的管理器
/// </summary>
public class ReadWriteConnectionStringManager : IReadWriteConnectionStringManager
{
    /// <summary>
    /// 连接解析器
    /// </summary>
    private IConnectionStringResolver _connectionStringResolver;

    public DbOptions _options { get; set; }

    public ReadWriteConnectionStringManager(DbOptions dbOptions, IReadWriteConnectorFactory readWriteConnectorFactory)
    {
        _options = dbOptions;

        var masters = _options.ReadWriteSeparationOptions.Master;
        var slaves = _options.ReadWriteSeparationOptions.Slave;
        var strategy = _options.ReadWriteSeparationOptions.ReadStrategy;

        //使用工厂创建连接器
        var readWriteConnectors = new List<IReadWriteConnector>();
        var mConnector = readWriteConnectorFactory.CreateConnector(strategy, NodeType.Master, masters);
        var sConnector = readWriteConnectorFactory.CreateConnector(strategy, NodeType.Slave, slaves);

        //追加主从连接器
        readWriteConnectors.Add(mConnector);
        readWriteConnectors.Add(sConnector);

        //实例化链接字符串解析器
        _connectionStringResolver = new ReadWriteConnectionStringResolver(readWriteConnectors, strategy, readWriteConnectorFactory);
      
    }

    /// <summary>
    /// 根据节点类型获取连接字符串
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <returns></returns>
    public IEnumerable<string> GetAllConnectionString(NodeType nodeType)
    {
        return _connectionStringResolver.GetAllConnectionString(nodeType);
    }

    /// <summary>
    /// 根据节点类型和名称获取节点连接字符串
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <param name="nodeName">节点名称</param>
    /// <returns></returns>
    public string GetConnectionString(NodeType nodeType, string nodeName = "")
    {
        //如果连接器不包含节点类型时
        //if (!_connectionStringResolver.ContainsReadWriteDataSourceName(nodeType))

        return _connectionStringResolver.GetConnectionString(nodeType, nodeName = "");
    }

    /// <summary>
    ///  添加连接字符串
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <param name="connectionString">字符串</param>
    /// <param name="nodeName">节点名称</param>
    /// <returns></returns>
    public bool AddConnectionString(NodeType nodeType, string connectionString, string nodeName = "")
    {
        return _connectionStringResolver.AddConnectionString(nodeType, connectionString, nodeName);
    }
}
