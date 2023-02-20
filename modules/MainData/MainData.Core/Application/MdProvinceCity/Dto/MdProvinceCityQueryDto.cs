using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;

namespace CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;

/// <summary>
/// 省份城市 D_ProvinceCity查询模型
/// </summary>
public class MdProvinceCityQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ProvinceCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ProvinceNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CityCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CityNm { get; set; }

}