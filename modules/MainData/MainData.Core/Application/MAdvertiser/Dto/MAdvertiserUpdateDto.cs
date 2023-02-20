
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;

/// <summary>
/// 广告商更新模型
/// </summary>
[ObjectMap(typeof(MAdvertiserEntity), true)]
public class MAdvertiserUpdateDto : MAdvertiserAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择广告商")]
    [Required(ErrorMessage = "请选择要修改的广告商")]
    public Guid Id { get; set; }
}