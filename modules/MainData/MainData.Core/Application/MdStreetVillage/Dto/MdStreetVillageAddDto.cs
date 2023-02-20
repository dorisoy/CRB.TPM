using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;

namespace CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;

/// <summary>
/// 街道村 D_StreetVillage添加模型
/// </summary>
[ObjectMap(typeof(MdStreetVillageEntity))]

public class MdStreetVillageAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string StreetCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string StreetNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string VillageCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string VillageNm { get; set; }

}