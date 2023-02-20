
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite
{
    /// <summary>
    /// 返写CRM记录
    /// </summary>
    [Table("CRM_Rewrite")]
    public partial class CrmRewriteEntity : Entity<Guid>
    {
        /// <summary>
        /// CODE1
        /// </summary>
		[Nullable]
        [Length(200)]
        public string PARTNER1 { get; set; } = string.Empty;

        /// <summary>
        /// CODE2
        /// </summary>
		[Nullable]
        [Length(200)]
        public string PARTNER2 { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ZUSER { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ZTYPE { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ZTYPE_1 { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ZDATE { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ZBAIOS { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = System.DateTime.Now;

    }
}
