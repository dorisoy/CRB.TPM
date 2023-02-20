
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty.Dto;

/// <summary>
/// 营销产品属性更新模型
/// </summary>
[ObjectMap(typeof(MProductMarketingPropertyEntity), true)]
public class MProductMarketingPropertyUpdateDto : MProductMarketingPropertyAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择营销产品属性")]
    [Required(ErrorMessage = "请选择要修改的营销产品属性")]
    public Guid Id { get; set; }
}