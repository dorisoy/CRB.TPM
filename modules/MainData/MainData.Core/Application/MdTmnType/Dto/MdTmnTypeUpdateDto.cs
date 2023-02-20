
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;

namespace CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;

/// <summary>
/// 终端类型（一二三级） M_TmnType更新模型
/// </summary>
[ObjectMap(typeof(MdTmnTypeEntity), true)]
public class MdTmnTypeUpdateDto : MdTmnTypeAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择终端类型（一二三级） M_TmnType")]
    [Required(ErrorMessage = "请选择要修改的终端类型（一二三级） M_TmnType")]
    public Guid Id { get; set; }
}