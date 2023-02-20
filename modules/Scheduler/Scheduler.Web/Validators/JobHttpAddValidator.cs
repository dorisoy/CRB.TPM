using CRB.TPM.Mod.Scheduler.Core.Application.JobHttp.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;
using CRB.TPM.Validation.FluentValidation;
using FluentValidation;

namespace CRB.TPM.Mod.Scheduler.Web.Validators;

public class JobHttpAddValidator : JobBaseValidator<JobHttpAddDto>
{
    public JobHttpAddValidator()
    {
        RuleFor(x => x.Token).Required().When(x => x.AuthType == AuthType.Jwt).WithMessage("Jwt认证需要设置token");
    }
}
