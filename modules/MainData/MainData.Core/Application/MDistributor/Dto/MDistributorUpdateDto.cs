
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

/// <summary>
/// 经销商/分销商更新模型
/// </summary>
[ObjectMap(typeof(MDistributorEntity), true)]
public class MDistributorUpdateDto : MDistributorAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择经销商/分销商")]
    [Required(ErrorMessage = "请选择要修改的经销商/分销商")]
    public Guid Id { get; set; }
}