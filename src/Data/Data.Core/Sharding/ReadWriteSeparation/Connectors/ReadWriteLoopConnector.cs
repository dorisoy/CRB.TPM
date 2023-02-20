using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
///用于表示轮询连接器
/// </summary>
public class ReadWriteLoopConnector: ReadWriteConnector
{
    private long _seed = 0;

    public ReadWriteLoopConnector(NodeType nodeType, Node[] nodes) :base(nodeType, nodes)
    {
    }

    /// <summary>
    /// 获取轮询字符串
    /// </summary>
    /// <returns></returns>
    private string DoGetNoReadNameConnectionString()
    {
        if (Length == 1)
            return Nodes[0].ConnectionString;

        var newValue = Interlocked.Increment(ref _seed);
        var next = (int)(newValue % Length);

        if (next < 0)
            return Nodes[Math.Abs(next)].ConnectionString;

        return Nodes[next].ConnectionString;
    }

    /// <summary>
    /// 获取轮询字符串
    /// </summary>
    /// <param name="nodeType"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public override string DoGetConnectionString(NodeType? nodeType,string nodeName = "")
    {
        if (string.IsNullOrEmpty(nodeName))
        {
            return DoGetNoReadNameConnectionString();
        }
        else
        {
            return Nodes.FirstOrDefault(o => o.Name == nodeName)?.ConnectionString ??
                   throw new Exception($"节点名称 :[{nodeName}] 不存在");
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
