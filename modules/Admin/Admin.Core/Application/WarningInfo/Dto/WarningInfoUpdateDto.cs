using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;

[ObjectMap(typeof(WarningInfoEntity), true)]
public class WarningInfoUpdateDto : WarningInfoAddDto
{
    [Required(ErrorMessage = "请选择项目")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择项目")]
    public Guid Id { get; set; }
}

