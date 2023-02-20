
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress
{
    /// <summary>
    /// 广告商地点表 M_ADAddress
    /// </summary>
    [Table("M_AdvertiserAddress")]
    public partial class MAdvertiserAddressEntity : Entity<Guid>
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
		[Nullable]
        [Length(80)]
        public string ADDRDESC { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string ZDESC { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string MAINPLACE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ORGID { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ORGCODE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string ORGNAME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string LOCSTATU { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string INVDATE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string FKTJ { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string COA { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string EXPACOUNT { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string ADVPACOUNT { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string FUACOUNT { get; set; } = string.Empty;

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

    }
}
