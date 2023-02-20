using System;
using System.Collections.Generic;
using System.Linq;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 表示SQL领域实体
    /// </summary>
    public class SqlFieldEntity
    {
        public SqlFieldEntity(TableEntity entity, string leftChar, string rightChart, string symbol)
        {
            PrimaryKey = entity.PrimaryKey;
            IsIdentity = entity.IsIdentity;
            PrimaryKeyType = entity.PrimaryKeyType;

            IgnoreFieldList = entity.IgnoreColumnList;
            OtherFieldDict = entity.OtherColumnDict;

            AllFieldList = entity.ColumnList.Select(s => s.Name);
            AllFieldExceptKeyList = AllFieldList.Where(w => w.ToLower() != entity.PrimaryKey.ToLower());

            AllFields = CommonUtil.GetFieldsStr(AllFieldList, leftChar, rightChart);
            AllFieldsAt = CommonUtil.GetFieldsAtStr(AllFieldList, symbol, this);
            AllFieldsAtEq = CommonUtil.GetFieldsAtEqStr(AllFieldList, leftChar, rightChart, symbol, this);

            AllFieldsExceptKey = CommonUtil.GetFieldsStr(AllFieldExceptKeyList, leftChar, rightChart);
            AllFieldsAtExceptKey = CommonUtil.GetFieldsAtStr(AllFieldExceptKeyList, symbol, this);
            AllFieldsAtEqExceptKey = CommonUtil.GetFieldsAtEqStr(AllFieldExceptKeyList, leftChar, rightChart, symbol, this);

        }


        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; }

        /// <summary>
        /// 主键类型
        /// </summary>
        public Type PrimaryKeyType { get; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public IEnumerable<string> AllFieldList { get; }

        /// <summary>
        /// 排除字段列表
        /// </summary>
        public IEnumerable<string> AllFieldExceptKeyList { get; }

        /// <summary>
        /// 忽略字段列表
        /// </summary>
        public IEnumerable<string> IgnoreFieldList { get; }

        /// <summary>
        /// 其他字段字典
        /// </summary>
        public Dictionary<string, double> OtherFieldDict { get; }

        /// <summary>
        /// 保留主键,所有列逗号分隔[name],[sex]
        /// </summary>
        public string AllFields { get; }

        public string AllFieldsAt { get; } //@name,@sex

        public string AllFieldsAtEq { get; }//[name]=@name,[sex]=@sex

        /// <summary>
        /// 去除主键,所有列逗号分隔[name],[sex]
        /// </summary>
        public string AllFieldsExceptKey { get; }

        public string AllFieldsAtExceptKey { get; } //@name,@sex

        public string AllFieldsAtEqExceptKey { get; }//[name]=@name,[sex]=@sex

    }
}
