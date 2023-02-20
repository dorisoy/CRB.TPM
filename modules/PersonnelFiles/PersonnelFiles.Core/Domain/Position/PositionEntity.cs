using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.PS.Core.Domain.Position;

/// <summary>
/// 职位
/// </summary>
[Table("Position")]
public partial class PositionEntity : EntityBase<Guid>
{
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
    /// 编码
    /// </summary>
    [Nullable]
    public string Code { get; set; }
}
