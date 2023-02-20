
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;

namespace CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;

/// <summary>
/// 街道村 D_StreetVillage更新模型
/// </summary>
[ObjectMap(typeof(MdStreetVillageEntity), true)]
public class MdStreetVillageUpdateDto : MdStreetVillageAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择街道村 D_StreetVillage")]
    [Required(ErrorMessage = "请选择要修改的街道村 D_StreetVillage")]
    public Guid Id { get; set; }
}