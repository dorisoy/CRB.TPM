using System;
using System.Collections.Generic;
using System.Text;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation
{
    /// <summary>
    /// 读写分离链接字符串解析
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// 连接器是否以及包含节点类型
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        bool ContainsReadWriteDataSourceName(NodeType nodeType);

        /// <summary>
        /// 获取指定数据源的读连接名称节点
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="nodeName">名称不存在报错,如果为null那么就随机获取</param>
        /// <returns></returns>
        string GetConnectionString(NodeType nodeType, string nodeName = "");

        /// <summary>
        /// 获取指定数据源的读连接名称节点
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        IEnumerable<string> GetAllConnectionString(NodeType nodeType);

        /// <summary>
        /// 添加数据源从库读字符串
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="connectionString"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        bool AddConnectionString(NodeType nodeType, string connectionString, string nodeName = "");
    }
}
