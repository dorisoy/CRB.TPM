using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using FluentValidation;
using System;

namespace CRB.TPM.Mod.Scheduler.Web.Validators;

public class JobUpdateValidator : JobBaseValidator<JobUpdateDto>
{
}
