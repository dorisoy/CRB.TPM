using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.PS.Core.Domain.Post;

/// <summary>
/// 岗位
/// </summary>
[Table("Post")]
public partial class PostEntity : EntityBase<Guid>
{
    /// <summary>
    /// 职位编号
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Length(100)]
    public string Name { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
		[Nullable]
    public string ShortName { get; set; }

    /// <summary>
    /// 职责
    /// </summary>
		[Nullable]
    [Length(500)]
    public string Responsibility { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
		[Nullable]
    [Length(500)]
    public string Remarks { get; set; }
}
