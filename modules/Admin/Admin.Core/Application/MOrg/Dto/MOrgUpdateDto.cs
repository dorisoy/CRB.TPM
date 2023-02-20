
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

/// <summary>
/// 组织表更新模型
/// </summary>
[ObjectMap(typeof(MOrgEntity), true)]
public class MOrgUpdateDto : MOrgAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择组织表")]
    [Required(ErrorMessage = "请选择要修改的组织表")]
    public Guid Id { get; set; }
}