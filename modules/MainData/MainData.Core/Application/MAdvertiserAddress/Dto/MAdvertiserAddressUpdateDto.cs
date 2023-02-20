
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;

/// <summary>
/// 广告商地点表 M_ADAddress更新模型
/// </summary>
[ObjectMap(typeof(MAdvertiserAddressEntity), true)]
public class MAdvertiserAddressUpdateDto : MAdvertiserAddressAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择广告商地点表 M_ADAddress")]
    [Required(ErrorMessage = "请选择要修改的广告商地点表 M_ADAddress")]
    public Guid Id { get; set; }
}