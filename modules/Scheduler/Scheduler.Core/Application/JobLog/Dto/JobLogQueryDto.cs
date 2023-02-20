using CRB.TPM.Data.Abstractions.Query;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;

public class JobLogQueryDto : QueryDto
{
    /// <summary>
    /// 任务编号
    /// </summary>
    [Required(ErrorMessage = "请指定一个任务")]
    public Guid JobId { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndDate { get; set; }
}
