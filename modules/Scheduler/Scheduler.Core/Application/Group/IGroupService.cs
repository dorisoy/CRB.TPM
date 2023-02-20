using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Group;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Group
{
    public interface IGroupService
    {
        Task<IResultModel> Add(GroupAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<PagingQueryResultModel<GroupEntity>> Query(GroupQueryDto dto);
        Task<IResultModel> Select();
    }
}