using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CRB.TPM.Utils.Helpers;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 用于表示随机连接器
/// </summary>
public class ReadWriteRandomConnector : ReadWriteConnector
{
    public ReadWriteRandomConnector(NodeType nodeType, Node[] nodes) : base(nodeType, nodes)
    {
    }

    /// <summary>
    /// 获取随机连接字符串
    /// </summary>
    /// <returns></returns>
    private string DoGetNoReadNameConnectionString()
    {
        if (Length == 1)
            return Nodes[0].ConnectionString;
        var next = RandomHelper.Next(0, Length);
        return Nodes[next].ConnectionString;
    }

    /// <summary>
    /// 根据节点名获取随机连接字符串
    /// </summary>
    /// <param name="nodeType"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public override string DoGetConnectionString(NodeType? nodeType, string nodeName = "")
    {
        if (string.IsNullOrEmpty(nodeName))
        {
            return DoGetNoReadNameConnectionString();
        }
        else
        {
            return Nodes.FirstOrDefault(o => o.Name == nodeName)?.ConnectionString ??
                throw new Exception($"Node name :[{nodeName}] not found");
        }
    }


    /// <summary>
    /// 获取节点全部连接
    /// </summary>
    /// <param name="nodeType"></param>
    /// <returns></returns>
    public override IEnumerable<string> DoGetAllConnectionString(NodeType? nodeType)
    {
        return Nodes.Select(s => s.ConnectionString);
    }
}
