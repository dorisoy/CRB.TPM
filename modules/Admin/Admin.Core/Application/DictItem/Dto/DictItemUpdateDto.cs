using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.DictItem.Dto;

[ObjectMap(typeof(DictItemEntity), true)]
public class DictItemUpdateDto : DictItemAddDto
{
    [Required(ErrorMessage = "请选择要修改的字典项")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的字典项")]
    public Guid Id { get; set; }
}