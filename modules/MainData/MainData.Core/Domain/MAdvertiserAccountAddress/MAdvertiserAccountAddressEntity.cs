
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress
{
    /// <summary>
    /// 广告商地点分配表 M_Re_ADAddressAccount
    /// </summary>
    [Table("M_AdvertiserAccount_Address")]
    public partial class MAdvertiserAccountAddressEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Length(40)]
        public string ADDRNO { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserAccountId { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ASSIGNSTAU { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string UUID { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string STDATE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ENDATE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string CODE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string MAINCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string BUCODE { get; set; } = string.Empty;

    }
}
