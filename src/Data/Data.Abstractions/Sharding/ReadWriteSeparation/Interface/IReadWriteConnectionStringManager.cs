using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{

    /// <summary>
    /// 表示用于读写连接字符串的管理器
    /// </summary>
    public interface IReadWriteConnectionStringManager
    {
        /// <summary>
        /// 根据节点类型和名称获取节点连接字符串
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        string GetConnectionString(NodeType nodeType, string nodeName = "");

        /// <summary>
        /// 添加连接字符串
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="connectionString"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        bool AddConnectionString(NodeType nodeType, string connectionString, string nodeName = "");

        /// <summary>
        /// 根据节点类型和名称获取节点连接字符串
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        IEnumerable<string> GetAllConnectionString(NodeType nodeType);
    }
}
