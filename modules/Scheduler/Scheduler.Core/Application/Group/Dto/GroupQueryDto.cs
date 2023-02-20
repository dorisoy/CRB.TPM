using CRB.TPM.Data.Abstractions.Query;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;

public class GroupQueryDto : QueryDto
{
    public string Name { get; set; }

    public string Code { get; set; }
}
