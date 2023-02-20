using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 表示用于读写连接器
    /// </summary>
    public interface IReadWriteConnector
    {
        public NodeType? NodeType { get; set; }

        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <param name="nodeType">类型</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public string GetConnectionString(NodeType? nodeType, string nodeName = "");

        /// <summary>
        /// 获取全部链接字符串
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllConnectionString(NodeType? nodeType);

        /// <summary>
        /// 动态添加链接字符串
        /// </summary>
        /// <param name="nodeType">类型</param>
        /// <param name="connectionString"></param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public bool AddConnectionString(NodeType? nodeType, string connectionString, string nodeName = "");
    }
}
