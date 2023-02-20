using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;

namespace CRB.TPM.Mod.MainData.Core.Application.MdSaleLine.Dto;

/// <summary>
/// 业务线 D_SaleLine添加模型
/// </summary>
[ObjectMap(typeof(MdSaleLineEntity))]
public class MdSaleLineAddDto
{
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
    public int Status { get; set; }

}