using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.MenuGroup;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.MenuGroup.Dto;

[ObjectMap(typeof(MenuGroupEntity), true)]
public class MenuGroupUpdateDto : MenuGroupAddDto
{
    [Required(ErrorMessage = "请选择要修改的分组")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的分组")]
    public Guid Id { get; set; }
}