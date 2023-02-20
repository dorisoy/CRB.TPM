using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 用于表示读写分离上下文单例
    /// </summary>
    public class ReadWriteContext
    {
        public bool DefaultReadEnable { get; set; }
        public int DefaultPriority { get; set; }

        private readonly Dictionary<NodeType?/*数据源*/, string /*数据源对应的读节点名称*/> _dataSourceReadNode;

        private ReadWriteContext()
        {
            DefaultReadEnable = false;
            DefaultPriority = 0;
            _dataSourceReadNode = new Dictionary<NodeType?, string>();
        }

        /// <summary>
        ///  创建上下文 ReadWriteContext 实例
        /// </summary>
        /// <returns></returns>
        public static ReadWriteContext Create()
        {
            return new ReadWriteContext();
        }

        /// <summary>
        /// 添加数据源对应读节点名称
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public bool AddDataSourceReadNode(NodeType?  nodeType, string nodeName)
        {
            if (_dataSourceReadNode.ContainsKey(nodeType))
                return false;

            _dataSourceReadNode.Add(nodeType, nodeName);
            return true;
        }

        /// <summary>
        /// 尝试获取对应数据源的节点名称
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <returns></returns>
        public string TryGetDataSourceReadNode(NodeType? nodeType)
        {
            string nodeName = "";
            _dataSourceReadNode.TryGetValue(nodeType, out nodeName);
            return nodeName;
        }
    }
}
