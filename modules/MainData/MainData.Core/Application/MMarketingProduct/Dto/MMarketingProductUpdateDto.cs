
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;

/// <summary>
/// 营销中心产品更新模型
/// </summary>
[ObjectMap(typeof(MMarketingProductEntity), true)]
public class MMarketingProductUpdateDto : MMarketingProductAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择营销中心产品")]
    [Required(ErrorMessage = "请选择要修改的营销中心产品")]
    public Guid Id { get; set; }
}