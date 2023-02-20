using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobLog;

/// <summary>
/// 任务日期
/// </summary>
[Table("Job_Log")]
public class JobLogEntity : EntityBase<Guid>
{
    /// <summary>
    /// 任务编号
    /// </summary>
    public Guid JobId { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public JobLogType Type { get; set; }

    /// <summary>
    /// 内容信息
    /// </summary>
    [MaxLength]
    public string Msg { get; set; }
}
