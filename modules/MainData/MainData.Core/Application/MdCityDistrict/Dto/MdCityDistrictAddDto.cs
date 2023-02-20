using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;

/// <summary>
/// 城市区县 D_CityDistrict添加模型
/// </summary>
[ObjectMap(typeof(MdCityDistrictEntity))]
public class MdCityDistrictAddDto
{
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