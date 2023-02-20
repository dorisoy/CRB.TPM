using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Utils.Annotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.PS.Core.Application.Employee.Dto;

[ObjectMap(typeof(EmployeeEntity), true)]
public class EmployeeUpdateDto : EmployeeAddDto
{

    [Required(ErrorMessage = "请选择人员")]
    //[Range(1, int.MaxValue, ErrorMessage = "请选择人员")]
    public Guid Id { get; set; }

}