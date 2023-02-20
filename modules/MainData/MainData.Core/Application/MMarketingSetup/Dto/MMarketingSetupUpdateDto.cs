
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;

/// <summary>
/// 营销中心配置更新模型
/// </summary>
[ObjectMap(typeof(MMarketingSetupEntity), true)]
public class MMarketingSetupUpdateDto : MMarketingSetupAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择营销中心配置")]
    [Required(ErrorMessage = "请选择要修改的营销中心配置")]
    public Guid Id { get; set; }
}