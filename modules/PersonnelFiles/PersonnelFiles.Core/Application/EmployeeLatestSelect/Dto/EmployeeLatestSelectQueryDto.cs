using CRB.TPM.Data.Abstractions.Query;


namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;

public class EmployeeLatestSelectQueryDto : QueryDto
{
    /// <summary>
    /// 人员名称
    /// </summary>
    public string Name { get; set; }
}
