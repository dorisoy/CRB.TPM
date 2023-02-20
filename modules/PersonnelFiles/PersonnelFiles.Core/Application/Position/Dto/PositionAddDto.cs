using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.PS.Core.Application.Position.Dto;

[ObjectMap(typeof(PositionEntity))]
public class PositionAddDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "请输入岗位名称")]
    public string Name { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}