
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince
{
    /// <summary>
    /// 国家省份 D_CountryProvince
    /// </summary>
    [Table("MD_CountryProvince")]
    public partial class MdCountryProvinceEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        [Length(3)]
        public string CountryCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(15)]
        public string CountryNm { get; set; } = string.Empty;

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

    }
}
