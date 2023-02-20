
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;

/// <summary>
/// 终端与经销商的关系信息更新模型
/// </summary>
[ObjectMap(typeof(MTerminalUserEntity), true)]
public class MTerminalUserUpdateDto : MTerminalUserAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择终端与经销商的关系信息")]
    [Required(ErrorMessage = "请选择要修改的终端与经销商的关系信息")]
    public Guid Id { get; set; }
}