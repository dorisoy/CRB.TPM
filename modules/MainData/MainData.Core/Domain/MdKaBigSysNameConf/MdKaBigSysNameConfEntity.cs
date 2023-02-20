
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf
{
    /// <summary>
    /// KA大系统 M_KABigSysNameConf
    /// </summary>
    [Table("MD_KABigSysNameConf")]
    public partial class MdKaBigSysNameConfEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string SaleOrg { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string KASystemNum { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string SaleOrgNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(60)]
        public string KASystemName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(6)]
        public string KALx { get; set; } = string.Empty;

    }
}
