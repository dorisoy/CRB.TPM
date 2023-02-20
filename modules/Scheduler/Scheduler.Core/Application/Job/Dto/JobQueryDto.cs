using CRB.TPM.Data.Abstractions.Query;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;

public class JobQueryDto : QueryDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}
