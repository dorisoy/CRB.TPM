using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;

namespace CRB.TPM.Mod.Admin.Core.Domain.Dict;

/// <summary>
/// 数据字典
/// </summary>
[Table("SYS_Dict")]
public partial class DictEntity : EntityBaseSoftDelete
{
    /// <summary>
    /// 分组编码
    /// </summary>
    public string GroupCode { get; set; }

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