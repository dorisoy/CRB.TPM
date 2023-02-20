using CRB.TPM.Data.Abstractions.Query;

namespace CRB.TPM.Mod.Admin.Core.Application.DictGroup.Dto;

public class DictGroupQueryDto : QueryDto
{
    /// <summary>
    /// 分组名称
    /// </summary>
    public string Name { get; set; }
}