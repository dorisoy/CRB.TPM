using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.Dict;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Application.Dict.Dto;

[ObjectMap(typeof(DictEntity))]
public class DictAddDto
{
    /// <summary>
    /// 分组编码
    /// </summary>
    [Required(ErrorMessage = "请选择分组")]
    public string GroupCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "请输入分组名称")]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "请输入分组编码")]
    public string Code { get; set; }
}