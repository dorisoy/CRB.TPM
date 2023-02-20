using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;

namespace CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;

/// <summary>
/// 终端类型（一二三级） M_TmnType添加模型
/// </summary>
[ObjectMap(typeof(MdTmnTypeEntity))]
public class MdTmnTypeAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string RegionCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string LineCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string LineNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level1TypeCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level1TypeNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level2TypeCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level2TypeNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level3TypeCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Level3TypeNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string MarketOrgCD { get; set; }

}