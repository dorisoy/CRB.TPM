using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.DictGroup;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Application.DictGroup.Dto;

[ObjectMap(typeof(DictGroupEntity))]
public class DictGroupAddDto
{
    /// <summary>
    /// 分组名称
    /// </summary>
    [Required(ErrorMessage = "请输入字典分组名称")]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "请输入字典分组唯一编码")]
    public string Code { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }
}