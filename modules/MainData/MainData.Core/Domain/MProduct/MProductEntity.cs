
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MProduct
{
    /// <summary>
    /// 
    /// </summary>
    [Table("M_Product")]
    public partial class MProductEntity : EntityBaseSoftDelete<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
		[Nullable]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
		[Nullable]
        [Length(200)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int? BottleBox { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int? Capacity { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string VolumeName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string BrandName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string OutPackName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string InPackName { get; set; } = string.Empty;

        /// <summary>
        /// 1（17位码）；2（11位码）；3（促销品）……
        /// </summary>
        public int? ProductType { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        public string ProductSpecName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Precision]
        public decimal? LitreConversionRate { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Length(1)]
        public int Enabled { get; set; }

        /// <summary>
        /// 17位对应的11位码产品id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 集团码
        /// </summary>
        [Length(20)]
        public string GroupCode { get; set; } = string.Empty;

        /// <summary>
        /// 集团名称
        /// </summary>
		[Nullable]
        [Length(200)]
        public string GroupName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 特征码
        /// </summary>
		[Nullable]
        [Length(10)]
        public string CharacterCode { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(500)]
        public string Remark { get; set; } = string.Empty;
    }
}
