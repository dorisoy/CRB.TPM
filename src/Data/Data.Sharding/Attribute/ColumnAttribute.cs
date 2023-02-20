using System;

namespace CRB.TPM.Data.Sharding
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 字段长度
        /// </summary>
        public double Length;

        /// <summary>
        /// 列注释
        /// </summary>
        public string Comment;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string ColumnType;

        public ColumnAttribute(double length = 0, string comment = null, string columnType = null)
        {
            Length = length;
            Comment = comment;
            ColumnType = columnType;
        }


    }
}
