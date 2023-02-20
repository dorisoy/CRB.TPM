using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.DictGroup;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.DictGroup.Dto;

[ObjectMap(typeof(DictGroupEntity), true)]
public class DictGroupUpdateDto : DictGroupAddDto
{
    [Required(ErrorMessage = "请选择要修改的分组")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的分组")]
    public Guid Id { get; set; }
}