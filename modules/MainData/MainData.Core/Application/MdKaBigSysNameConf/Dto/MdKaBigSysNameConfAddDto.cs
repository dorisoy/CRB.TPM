using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;

/// <summary>
/// KA大系统 M_KABigSysNameConf添加模型
/// </summary>
[ObjectMap(typeof(MdKaBigSysNameConfEntity))]

public class MdKaBigSysNameConfAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string SaleOrg { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string KASystemNum { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string SaleOrgNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string KASystemName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string KALx { get; set; }

}