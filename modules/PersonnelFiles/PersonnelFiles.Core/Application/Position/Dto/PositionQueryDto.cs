using CRB.TPM.Data.Abstractions.Query;


namespace CRB.TPM.Mod.PS.Core.Application.Position.Dto;

public class PositionQueryDto : QueryDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}
