using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MEntity;

namespace CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;

/// <summary>
/// 业务实体添加模型
/// </summary>
[ObjectMap(typeof(MEntityEntity))]
public class MEntityAddDto
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "法人主体编码最长30位")]
    public string EntityCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "法人主体名称最长40位")]
    public string EntityName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required]
    public bool Enabled { get; set; }

    /// <summary>
    /// 用于上传OCMS
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "用于上传OCMS最长30位")]
    public string ERPCode { get; set; }

}