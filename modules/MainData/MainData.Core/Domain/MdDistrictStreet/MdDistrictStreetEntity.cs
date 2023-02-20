
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet
{
    /// <summary>
    /// 区县街道 D_DistrictStreet
    /// </summary>
    [Table("MD_DistrictStreet")]
    public partial class MdDistrictStreetEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string DistrictCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string DistrictNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string StreetCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string StreetNm { get; set; } = string.Empty;

    }
}
