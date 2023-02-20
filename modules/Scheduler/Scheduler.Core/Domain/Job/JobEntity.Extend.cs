using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Sharding;
using System;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.Job;

public partial class JobEntity
{
    /// <summary>
    /// 任务类型名称
    /// </summary>
    [Ignore]
    public string TypeName => JobType.ToDescription();
}
