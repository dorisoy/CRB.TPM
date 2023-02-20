
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;

namespace CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;

/// <summary>
/// 省份城市 D_ProvinceCity更新模型
/// </summary>
[ObjectMap(typeof(MdProvinceCityEntity), true)]
public class MdProvinceCityUpdateDto : MdProvinceCityAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择省份城市 D_ProvinceCity")]
    [Required(ErrorMessage = "请选择要修改的省份城市 D_ProvinceCity")]
    public Guid Id { get; set; }
}