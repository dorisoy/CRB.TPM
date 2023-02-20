using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.PS.Core.Domain.Post;

/// <summary>
/// 岗位
/// </summary>
public partial class PostEntity
{
    /// <summary>
    /// 职位名称
    /// </summary>
    [NotMappingColumn]
    public string PositionName { get; set; }
}
