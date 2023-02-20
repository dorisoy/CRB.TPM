using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;

/// <summary>
/// 省份城市 D_ProvinceCity添加模型
/// </summary>
[ObjectMap(typeof(MdProvinceCityEntity))]
public class MdProvinceCityAddDto
{
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