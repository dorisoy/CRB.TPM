
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;

/// <summary>
/// 更新模型
/// </summary>
[ObjectMap(typeof(MProductEntity), true)]
public class MProductUpdateDto : MProductAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择")]
    [Required(ErrorMessage = "请选择要修改的")]
    public Guid Id { get; set; }
}