using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;

/// <summary>
/// 国家省份 D_CountryProvince查询模型
/// </summary>
public class MdCountryProvinceQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CountryCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CountryNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ProvinceCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ProvinceNm { get; set; }

}