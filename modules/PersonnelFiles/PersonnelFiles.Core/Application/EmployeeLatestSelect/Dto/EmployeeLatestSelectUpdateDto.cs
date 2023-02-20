using CRB.TPM.Mod.PS.Core.Domain.Department;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using CRB.TPM.Utils.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;

[ObjectMap(typeof(EmployeeLatestSelectEntity), true)]
public class EmployeeLatestSelectUpdateDto : EmployeeLatestSelectAddDto
{
    [Required(ErrorMessage = "请选择要修改的项目")]
    [Range(1, int.MaxValue, ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}