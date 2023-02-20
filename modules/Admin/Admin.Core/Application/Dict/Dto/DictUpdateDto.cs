using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.Dict;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;

[ObjectMap(typeof(DictEntity), true)]
public class DictUpdateDto : DictAddDto
{
    [Required(ErrorMessage = "请选择要修改的字典")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的字典")]
    public Guid Id { get; set; }
}