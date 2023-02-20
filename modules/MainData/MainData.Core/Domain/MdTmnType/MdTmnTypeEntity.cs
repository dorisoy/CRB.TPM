
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdTmnType
{
    /// <summary>
    /// 终端类型（一二三级） M_TmnType
    /// </summary>
    [Table("MD_TmnType")]
    public partial class MdTmnTypeEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string RegionCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string LineCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(40)]
        public string LineNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string Level1TypeCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string Level1TypeNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string Level2TypeCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string Level2TypeNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string Level3TypeCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string Level3TypeNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Length(8)]
        public string MarketOrgCD { get; set; } = string.Empty;

    }
}
