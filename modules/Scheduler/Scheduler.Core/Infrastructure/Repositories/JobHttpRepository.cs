using CRB.TPM.Data.Core.Repository;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;
using System;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Scheduler.Core.Infrastructure.Repositories;

public class JobHttpRepository : RepositoryAbstract<JobHttpEntity>, IJobHttpRepository
{
    public Task<JobHttpEntity> GetByJob(Guid jobId)
    {
        return Get(m => m.JobId == jobId);
    }
}
