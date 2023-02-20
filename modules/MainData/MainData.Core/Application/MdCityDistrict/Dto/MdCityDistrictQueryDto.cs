using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;

/// <summary>
/// 城市区县 D_CityDistrict查询模型
/// </summary>
public class MdCityDistrictQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CityCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CityNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DistrictCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DistrictNm { get; set; }

}