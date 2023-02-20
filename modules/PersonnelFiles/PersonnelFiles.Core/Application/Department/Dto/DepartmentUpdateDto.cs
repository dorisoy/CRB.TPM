using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.PS.Core.Application.Department.Dto;

[ObjectMap(typeof(DepartmentEntity), true)]
public class DepartmentUpdateDto : DepartmentAddDto
{
    [Required(ErrorMessage = "请选择要修改的部门")]
    [Range(1, int.MaxValue, ErrorMessage = "请选择要修改的部门")]
    public int Id { get; set; }
}