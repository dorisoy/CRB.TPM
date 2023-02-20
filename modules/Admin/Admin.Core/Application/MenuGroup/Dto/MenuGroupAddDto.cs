using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.MenuGroup;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Application.MenuGroup.Dto;

[ObjectMap(typeof(MenuGroupEntity))]
public class MenuGroupAddDto
{
    [Required(ErrorMessage = "请输入分组名称")]
    public string Name { get; set; }

    public string Remarks { get; set; }
}