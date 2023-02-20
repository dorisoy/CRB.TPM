
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct
{
    /// <summary>
    /// 营销中心产品
    /// </summary>
    [Table("M_Marketing_Product")]
    public partial class MMarketingProductEntity : Entity<Guid>
    {
        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Length(1)]
        public int Enabled { get; set; }
    }
}
