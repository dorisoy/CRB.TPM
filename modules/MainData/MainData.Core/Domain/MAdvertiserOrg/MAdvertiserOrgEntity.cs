
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg
{
    /// <summary>
    /// 广告商营销组织关系表 M_Re_Org_AD
    /// </summary>
    [Table("M_Advertiser_Org")]
    public partial class MAdvertiserOrgEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string MarketOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string BigAreaOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string OfficeOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string StationOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

    }
}
