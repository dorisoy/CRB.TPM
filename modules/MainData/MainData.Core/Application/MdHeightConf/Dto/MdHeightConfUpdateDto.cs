
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;

/// <summary>
/// 制高点配置 M_HeightConf更新模型
/// </summary>
[ObjectMap(typeof(MdHeightConfEntity), true)]
public class MdHeightConfUpdateDto : MdHeightConfAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择制高点配置 M_HeightConf")]
    [Required(ErrorMessage = "请选择要修改的制高点配置 M_HeightConf")]
    public Guid Id { get; set; }
}