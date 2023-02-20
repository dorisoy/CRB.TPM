using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;

namespace CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;

/// <summary>
/// 区县街道 D_DistrictStreet添加模型
/// </summary>
[ObjectMap(typeof(MdDistrictStreetEntity))]
public class MdDistrictStreetAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string DistrictCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string DistrictNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string StreetCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string StreetNm { get; set; }

}