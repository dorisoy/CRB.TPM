
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict
{
    /// <summary>
    /// 城市区县 D_CityDistrict
    /// </summary>
    [Table("MD_CityDistrict")]
    public partial class MdCityDistrictEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(10)]
        public string CityCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string CityNm { get; set; } = string.Empty;

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

    }
}
