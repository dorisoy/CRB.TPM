using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.PS.Core.Application.Position.Dto;

[ObjectMap(typeof(PositionEntity), true)]
public class PositionUpdateDto : PositionAddDto
{
    [Required(ErrorMessage = "请选择要修改的项目")]
    //[Range(1, int.MaxValue, ErrorMessage = "请选择要修改的项目")]
    public Guid Id { get; set; }

}