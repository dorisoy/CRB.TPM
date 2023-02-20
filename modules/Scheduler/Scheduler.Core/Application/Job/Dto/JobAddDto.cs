using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;

/// <summary>
/// 任务添加模型
/// </summary>
[ObjectMap(typeof(JobEntity))]
public class JobAddDto : JobBaseDto
{
    /// <summary>
    /// 任务类
    /// </summary>
    [Required(ErrorMessage = "请选择任务类")]
    public string JobClass { get; set; }

    /// <summary>
    /// 立即启动
    /// </summary>
    public bool Start { get; set; }

}
