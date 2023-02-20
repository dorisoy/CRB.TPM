using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;

namespace CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;

/// <summary>
/// 制高点配置 M_HeightConf添加模型
/// </summary>
[ObjectMap(typeof(MdHeightConfEntity))]
public class MdHeightConfAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string SaleOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Height { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Text { get; set; }

}