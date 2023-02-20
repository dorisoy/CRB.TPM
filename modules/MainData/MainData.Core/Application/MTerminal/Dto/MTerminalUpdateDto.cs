
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;

/// <summary>
/// 终端信息更新模型
/// </summary>
[ObjectMap(typeof(MTerminalEntity), true)]
public class MTerminalUpdateDto : MTerminalAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择终端信息")]
    [Required(ErrorMessage = "请选择要修改的终端信息")]
    public Guid Id { get; set; }
}