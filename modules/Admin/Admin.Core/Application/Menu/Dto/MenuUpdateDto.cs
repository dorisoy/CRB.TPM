using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.Menu.Dto;

[ObjectMap(typeof(MenuEntity), true)]
public class MenuUpdateDto : MenuAddDto
{
    [Required(ErrorMessage = "请选择要修改的菜单")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择要修改的菜单")]
    public Guid Id { get; set; }
}