
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDetail.Dto;

/// <summary>
/// 终端其他信息更新模型
/// </summary>
[ObjectMap(typeof(MTerminalDetailEntity), true)]
public class MTerminalDetailUpdateDto : MTerminalDetailAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择终端其他信息")]
    [Required(ErrorMessage = "请选择要修改的终端其他信息")]
    public Guid Id { get; set; }
}