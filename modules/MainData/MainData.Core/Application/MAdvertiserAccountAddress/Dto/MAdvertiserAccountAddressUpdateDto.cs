
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;

/// <summary>
/// 广告商地点分配表 M_Re_ADAddressAccount更新模型
/// </summary>
[ObjectMap(typeof(MAdvertiserAccountAddressEntity), true)]
public class MAdvertiserAccountAddressUpdateDto : MAdvertiserAccountAddressAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择广告商地点分配表 M_Re_ADAddressAccount")]
    [Required(ErrorMessage = "请选择要修改的广告商地点分配表 M_Re_ADAddressAccount")]
    public Guid Id { get; set; }
}