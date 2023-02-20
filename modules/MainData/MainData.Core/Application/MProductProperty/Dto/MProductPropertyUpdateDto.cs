
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;

/// <summary>
/// 产品属性更新模型
/// </summary>
[ObjectMap(typeof(MProductPropertyEntity), true)]
public class MProductPropertyUpdateDto : MProductPropertyAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择产品属性")]
    [Required(ErrorMessage = "请选择要修改的产品属性")]
    public Guid Id { get; set; }
}