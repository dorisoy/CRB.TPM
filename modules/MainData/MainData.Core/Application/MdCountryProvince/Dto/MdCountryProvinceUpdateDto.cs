
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;

/// <summary>
/// 国家省份 D_CountryProvince更新模型
/// </summary>
[ObjectMap(typeof(MdCountryProvinceEntity), true)]
public class MdCountryProvinceUpdateDto : MdCountryProvinceAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择国家省份 D_CountryProvince")]
    [Required(ErrorMessage = "请选择要修改的国家省份 D_CountryProvince")]
    public Guid Id { get; set; }
}