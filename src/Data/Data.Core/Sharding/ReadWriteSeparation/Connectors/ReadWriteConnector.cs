using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 表示读写连接器
/// </summary>
public abstract class ReadWriteConnector : IReadWriteConnector
{
    protected List<Node> Nodes { get; }

    protected int Length { get; private set; }

    public NodeType? NodeType { get; set; }

    private object slock = new object();

    //private readonly string _tempConnectionString;
    //private readonly OneByOneChecker _oneByOneChecker = new OneByOneChecker();

    //public ReadWriteConnector(Node[] writeNodes, Node[] readNodes)
    //{
    //    Masters = writeNodes.ToList();
    //    Slaves = readNodes.ToList();

    //    WriteLength = writeNodes.Count();
    //    ReadLength = readNodes.Count();

    //    //_tempConnectionString = ConnectionStrings[0];
    //}

    public ReadWriteConnector(NodeType? nodeType, Node[] nodes)
    {
        Nodes = nodes.ToList();
        Length = nodes.Count();
        NodeType = nodeType;

        //_tempConnectionString = ConnectionStrings[0];
    }


    /// <summary>
    /// 获取连接字符串
    /// </summary>
    /// <param name="nodeType"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public string GetConnectionString(NodeType? nodeType, string nodeName = "")
    {
        return DoGetConnectionString(nodeType, nodeName);
    }

    /// <summary>
    /// 获取指定类型连接字符串
    /// </summary>
    /// <param name="nodeType"></param>
    /// <returns></returns>
    public IEnumerable<string> GetAllConnectionString(NodeType? nodeType)
    {
        return DoGetAllConnectionString(nodeType);
    }


    public abstract string DoGetConnectionString(NodeType? nodeType, string nodeName = "");
    public abstract IEnumerable<string> DoGetAllConnectionString(NodeType? nodeType);


    /// <summary>
    /// 动态添加数据源
    /// </summary>
    /// <param name="nodeType">节点类型：Master，Slave</param>
    /// <param name="connectionString"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool AddConnectionString(NodeType? nodeType, string connectionString, string nodeName = "")
    {
        var acquired = Monitor.TryEnter(slock, TimeSpan.FromSeconds(3));
        if (!acquired)
            throw new Exception($"{nameof(AddConnectionString)} is busy");
        try
        {
            //switch (nodeType)
            //{
            //    case NodeType.Master:
            //        Masters.Add(new Node() { Name = readNodeName ?? Guid.NewGuid().ToString("n"), ConnectionString = connectionString });
            //        WriteLength = Masters.Count;
            //        break;
            //    case NodeType.Slave:
            //        Slaves.Add(new Node() { Name = readNodeName ?? Guid.NewGuid().ToString("n"), ConnectionString = connectionString });
            //        ReadLength = Slaves.Count;
            //        break;
            //}

            Nodes.Add(new Node() { Name = nodeName ?? Guid.NewGuid().ToString("n"), ConnectionString = connectionString });
            Length = Nodes.Count;
            return true;
        }
        finally
        {
            Monitor.Exit(slock);
        }
    }
}
