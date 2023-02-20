
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser
{
    /// <summary>
    /// 广告商
    /// </summary>
    [Table("M_Advertiser")]
    public partial class MAdvertiserEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string AdvertiserCode { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string AdvertiserName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string RegionCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        public string ADJC { get; set; } = string.Empty;

    }
}
