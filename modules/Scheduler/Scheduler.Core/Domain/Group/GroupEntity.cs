using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.Group;

/// <summary>
/// 任务组
/// </summary>
[Table("Group")]
public partial class GroupEntity  : EntityBase<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Length(100)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Length(100)]
    public string Code { get; set; }
}
