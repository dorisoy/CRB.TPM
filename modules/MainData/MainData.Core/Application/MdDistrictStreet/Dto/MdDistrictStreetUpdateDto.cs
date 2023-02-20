
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;

namespace CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;

/// <summary>
/// 区县街道 D_DistrictStreet更新模型
/// </summary>
[ObjectMap(typeof(MdDistrictStreetEntity), true)]
public class MdDistrictStreetUpdateDto : MdDistrictStreetAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择区县街道 D_DistrictStreet")]
    [Required(ErrorMessage = "请选择要修改的区县街道 D_DistrictStreet")]
    public Guid Id { get; set; }
}