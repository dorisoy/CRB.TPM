
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Logging.Core.Domain.CrmRelation
{
    /// <summary>
    /// CRM的关系变动记录表
    /// </summary>
    [Table("CRM_Relation")]
    public partial class CrmRelationEntity : Entity<Guid>
    {
        /// <summary>
        /// 编码1
        /// </summary>
        [Length(100)]
        [Nullable]
        public string Code1 { get; set; } = string.Empty;

        /// <summary>
        /// 编码2
        /// </summary>
        [Length(100)]
        [Nullable]
        public string Code2 { get; set; } = string.Empty;

        /// <summary>
        /// 关系类型
        /// </summary>
        [Length(20)]
        [Nullable]
        public string RELTYP { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型，D是删除；其他都可看作新增
        /// </summary>
        [Length(20)]
        [Nullable]
        public string ZUPDMODE { get; set; } = string.Empty;

        [Nullable]
        public string ZDATE { get; set; } = string.Empty;

    }
}
