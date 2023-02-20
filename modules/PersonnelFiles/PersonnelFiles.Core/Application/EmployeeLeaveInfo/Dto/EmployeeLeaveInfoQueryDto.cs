using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using System;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;

public class EmployeeLeaveInfoQueryDto : QueryDto
{
    /// <summary>
    /// 人员编号
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// 离职类型
    /// </summary>
    [Length(100)]
    public string Type { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    [Length(500)]
    [Nullable]
    public string Reason { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime ApplyDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime LeaveDate { get; set; }
}
