
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;

/// <summary>
/// 终端与经销商的关系信息更新模型
/// </summary>
[ObjectMap(typeof(MTerminalDistributorEntity), true)]
public class MTerminalDistributorUpdateDto : MTerminalDistributorAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择终端与经销商的关系信息")]
    [Required(ErrorMessage = "请选择要修改的终端与经销商的关系信息")]
    public Guid Id { get; set; }
}