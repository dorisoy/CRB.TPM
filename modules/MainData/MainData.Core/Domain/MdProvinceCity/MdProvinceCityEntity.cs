
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity
{
    /// <summary>
    /// 省份城市 D_ProvinceCity
    /// </summary>
    [Table("MD_ProvinceCity")]
    public partial class MdProvinceCityEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(3)]
        public string ProvinceCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(20)]
        public string ProvinceNm { get; set; } = string.Empty;

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

    }
}
