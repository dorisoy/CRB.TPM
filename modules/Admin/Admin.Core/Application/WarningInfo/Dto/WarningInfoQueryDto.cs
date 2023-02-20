using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;

namespace CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;

public class WarningInfoQueryDto : QueryDto
{
    public WarningInfoType Type { get; set; }
    public string OrgId { get; set; }
    public string OrgCodeName { get; set; }
}