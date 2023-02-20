
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Logging.Core.Domain.CrmData
{
    /// <summary>
    /// CRM客户、终端记录
    /// </summary>
    [Table("CRM_Data")]
    public partial class CrmDataEntity : Entity<Guid>
    {
        /// <summary>
        /// 数据类型，7-客户；8-终端
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 客户编码、终端编码
        /// </summary>
        [Length(100)]
        [Nullable]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// json值
        /// </summary>
		[Nullable]
        [Length(4000)]
        public string JsonString { get; set; } = string.Empty;

        /// <summary>
        /// crm时间
        /// </summary>
        public DateTime ZDATE { get; set; } = System.DateTime.Now;

    }
}
