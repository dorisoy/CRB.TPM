using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.PS.Core.Domain.Post;

/// <summary>
/// ��λ
/// </summary>
public partial class PostEntity
{
    /// <summary>
    /// ְλ����
    /// </summary>
    [NotMappingColumn]
    public string PositionName { get; set; }
}
