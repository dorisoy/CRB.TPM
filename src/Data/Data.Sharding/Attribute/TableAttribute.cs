using System;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 表属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 主键名
        /// </summary>
        public string PrimaryKey;

        /// <summary>
        /// 是否标识
        /// </summary>
        public bool IsIdentity;

        /// <summary>
        /// 表描述
        /// </summary>
        public string Comment;

        /// <summary>
        /// 表名称
        /// </summary>
        public string Engine;

        /// <summary>
        /// 集簇名称
        /// </summary>
        public string Cluster;

        /// <summary>
        /// 标记表属性
        /// </summary>
        /// <param name="primaryKey">主键名</param>
        /// <param name="isIdentity">是否标识</param>
        /// <param name="comment">描述</param>
        /// <param name="engine">表名称</param>
        /// <param name="cluster">集簇名称</param>

        public TableAttribute(string primaryKey, bool isIdentity = true, string comment = null, string engine = null, string cluster = null)
        {
            PrimaryKey = primaryKey;
            IsIdentity = isIdentity;
            Comment = comment;
            Engine = engine;
            Cluster = cluster;
        }

    }
}
