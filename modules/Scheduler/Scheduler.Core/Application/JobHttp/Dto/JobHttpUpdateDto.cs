using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.JobHttp.Dto;

public class JobHttpUpdateDto : JobHttpAddDto
{
    [Required(ErrorMessage = "请选择任务")]
    public Guid Id { get; set; }
}
