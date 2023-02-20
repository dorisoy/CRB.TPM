using CRB.TPM.Data.Abstractions.Query;
using System;

namespace CRB.TPM.Mod.PS.Core.Application.Employee.Dto;

public class EmployeeQueryDto : QueryDto
{
    /// <summary>
    /// 部门编号
    /// </summary>
    public Guid? DepartmentId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    public Guid? JobNo { get; set; }

    /// <summary>
    /// LDAP账号
    /// </summary>
    public string LDAP { get; set; }
}
