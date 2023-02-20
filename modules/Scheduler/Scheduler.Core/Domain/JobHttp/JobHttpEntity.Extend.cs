using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Sharding;
using System;


namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;

public partial class JobHttpEntity
{
    [Ignore]
    public string MethodName => Method.ToDescription();

    [Ignore]
    public string AuthTypeName => AuthType.ToDescription();

    [Ignore]
    public string ContentTypeName => ContentType.ToDescription();
}
