using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo
{
    public interface IEmployeeLeaveInfoSevice
    {
        Task<EmployeeLeaveInfoEntity> GetByEmployeeId(Guid employeeId);
        Task<PagingQueryResultModel<EmployeeLeaveInfoEntity>> Query(EmployeeLeaveInfoQueryDto dto);
    }
}