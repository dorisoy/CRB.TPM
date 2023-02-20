using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;

/// <summary>
/// 产品属性添加模型
/// </summary>
[ObjectMap(typeof(MProductPropertyEntity))]
public class MProductPropertyAddDto
{
    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    public int ProductPropertiesType { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [MaxLength(50, ErrorMessage = "产品属性编码最长50位")]
    public string ProductPropertiesCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100, ErrorMessage = "产品属性编码最长100位")]
    public string ProductPropertiesName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    [Required]
    public int Sort { get; set; }

}