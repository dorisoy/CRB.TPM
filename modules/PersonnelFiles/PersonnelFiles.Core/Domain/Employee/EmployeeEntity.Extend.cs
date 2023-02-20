using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.PS.Core.Domain.Employee;

public partial class EmployeeEntity
{
    /// <summary>
    /// 工号(1000000 + Id)
    /// </summary>
    [NotMappingColumn]
    public Guid JobNo => Guid.NewGuid();

    /// <summary>
    /// 部门路径
    /// </summary>
    [NotMappingColumn]
    public string DepartmentPath { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    [NotMappingColumn]
    public string PostName { get; set; }

    /// <summary>
    /// 性别名称
    /// </summary>
    [NotMappingColumn]
    public string SexName => Sex.ToDescription();

    /// <summary>
    /// 性质名称
    /// </summary>
    [NotMappingColumn]
    public string NatureName => Nature.ToDescription();

    /// <summary>
    /// 状态名称
    /// </summary>
    [NotMappingColumn]
    public string StatusName => Status.ToDescription();
}
