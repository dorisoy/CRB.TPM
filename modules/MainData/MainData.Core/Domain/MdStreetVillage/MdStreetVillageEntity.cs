
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage
{
    /// <summary>
    /// 街道村 D_StreetVillage
    /// </summary>
    [Table("MD_StreetVillage")]
    public partial class MdStreetVillageEntity : Entity<Guid>
    {
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

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string VillageCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string VillageNm { get; set; } = string.Empty;

    }
}
