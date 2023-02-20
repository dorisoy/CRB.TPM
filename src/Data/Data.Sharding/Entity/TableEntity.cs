using System;
using System.Collections.Generic;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 用于表示实体表
    /// </summary>
    public class TableEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 主键类型
        /// </summary>
        public Type PrimaryKeyType { get; set; }

        /// <summary>
        /// 是否自增标识
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 物理表名
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// 集簇名称
        /// </summary>
        public string Cluster { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public List<ColumnEntity> ColumnList { get; set; }

        /// <summary>
        /// 字典列
        /// </summary>
        public Dictionary<string, double> OtherColumnDict { get; set; }

       /// <summary>
       /// 索引列
       /// </summary>
        public List<IndexEntity> IndexList { get; set; }

        /// <summary>
        /// 忽略列
        /// </summary>
        public List<string> IgnoreColumnList { get; set; }
    }
}
