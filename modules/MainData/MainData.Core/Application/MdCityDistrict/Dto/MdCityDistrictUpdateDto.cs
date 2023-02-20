
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;

/// <summary>
/// 城市区县 D_CityDistrict更新模型
/// </summary>
[ObjectMap(typeof(MdCityDistrictEntity), true)]
public class MdCityDistrictUpdateDto : MdCityDistrictAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择城市区县 D_CityDistrict")]
    [Required(ErrorMessage = "请选择要修改的城市区县 D_CityDistrict")]
    public Guid Id { get; set; }
}