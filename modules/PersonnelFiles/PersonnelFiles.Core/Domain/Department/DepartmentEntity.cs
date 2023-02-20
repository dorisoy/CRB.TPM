using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.PS.Core.Domain.Department;

/// <summary>
/// 部门
/// </summary>
[Table("Department")]
public class DepartmentEntity : EntityBase<Guid>
{
    /// <summary>
    /// 父级ID
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Length(100)]
    public string Name { get; set; }

    /// <summary>
    /// 唯一编码
    /// </summary>
    [Nullable]
    [Length(200)]
    public string Code { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 完整路径
    /// </summary>
    public string FullPath { get; set; }

}
