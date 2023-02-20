using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;

public class JobUpdateDto : JobAddDto
{
    [Required(ErrorMessage = "请选择任务")]
    public Guid Id { get; set; }
}
