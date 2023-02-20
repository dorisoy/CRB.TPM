using System;
using CRB.TPM.Data.Abstractions.Query;

namespace CRB.TPM.Mod.PS.Core.Application.Department.Dto;

public class DepartmentQueryDto : QueryDto
{
    /// <summary>
    /// 父节点
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}
