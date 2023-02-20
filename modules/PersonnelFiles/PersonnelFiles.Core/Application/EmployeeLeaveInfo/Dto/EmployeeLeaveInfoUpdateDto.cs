using CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;
using CRB.TPM.Utils.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;

[ObjectMap(typeof(EmployeeLeaveInfoEntity), true)]
public class EmployeeLeaveInfoUpdateDto : EmployeeLeaveInfoAddDto
{
    [Required(ErrorMessage = "请选择要修改的项目")]
    [Range(1, int.MaxValue, ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}