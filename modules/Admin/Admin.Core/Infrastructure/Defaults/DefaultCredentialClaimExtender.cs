using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Defaults;

[ScopedInject]
internal class DefaultCredentialClaimExtender : ICredentialClaimExtender
{
    public Task Extend(List<Claim> claims, Guid accountId)
    {
        return Task.CompletedTask;
    }
}