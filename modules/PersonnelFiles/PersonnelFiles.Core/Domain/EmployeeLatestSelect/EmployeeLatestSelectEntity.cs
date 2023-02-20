using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;

/// <summary>
/// 最近选择
/// </summary>
[Table("Employee_Latest_Select")]
public class EmployeeLatestSelectEntity : Entity<Guid>
{
    /// <summary>
    /// 人员编号
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// 关联人员编号
    /// </summary>
    public Guid RelationId { get; set; }

    /// <summary>
    /// 最近选择时间戳
    /// </summary>
    public long Timestamp { get; set; }
}


public class EmployeeLatestSelectVo 
{

    /// <summary>
    /// 关联人员编号
    /// </summary>
    public Guid RelationId { get; set; }

    /// <summary>
    /// 最近选择时间戳
    /// </summary>
    public long Timestamp { get; set; }
}
