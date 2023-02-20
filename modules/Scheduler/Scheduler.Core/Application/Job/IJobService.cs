using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobHttp.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job
{
    public interface IJobService
    {
        Task<IResultModel> Add(JobAddDto dto);
        Task<IResultModel> AddHttpJob(JobHttpAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<IResultModel> Edit(Guid id);
        Task<IResultModel> EditHttpJob(Guid id);
        Task<IResultModel> JobHttpDetails(Guid id);
        Task<IResultModel> Log(JobLogQueryDto dto);
        Task<IResultModel> Pause(Guid id);
        Task<IResultModel> Query(JobQueryDto dto);
        Task<IResultModel> Resume(Guid id);
        Task<IResultModel> Stop(Guid id);
        Task<IResultModel> Update(JobUpdateDto model);
        Task<IResultModel> UpdateHttpJob(JobHttpUpdateDto model);
    }
}