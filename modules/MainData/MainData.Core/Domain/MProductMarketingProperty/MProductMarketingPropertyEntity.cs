
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty
{
    /// <summary>
    /// 营销产品属性
    /// </summary>
    [Table("M_ProductMarketingProperty")]
    public partial class MProductMarketingPropertyEntity : Entity<Guid>
    {
        /// <summary>
        /// 营销中心id（orgid）
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
		[Nullable]
        [Length(100)]
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 子品牌
        /// </summary>
		[Nullable]
        [Length(100)]
        public string BrandChild { get; set; } = string.Empty;

        /// <summary>
        /// 重点产品
        /// </summary>
		[Nullable]
        [Length(100)]
        public string KeyProduct { get; set; } = string.Empty;

        /// <summary>
        /// 产品简称
        /// </summary>
		[Nullable]
        [Length(100)]
        public string Abbreviation { get; set; } = string.Empty;

    }
}
