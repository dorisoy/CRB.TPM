using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Vo;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Employee
{
    public interface IEmployeeService
    {
        Task<IResultModel> Add(EmployeeAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<IResultModel> Edit(Guid id);
        Task<IResultModel> EditAccount(Guid id);
        Task<IResultModel> GetBaseInfoList(List<Guid> ids);
        Task<IResultModel> GetLeaveInfo(Guid id);
        Task<IResultModel> GetTree();
        Task<IResultModel> Leave(EmployeeLeaveVo model);
        Task<PagingQueryResultModel<EmployeeEntity>> Query(EmployeeQueryDto dto);
        Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> QueryLatestSelect(Guid accountId, EmployeeLatestSelectQueryDto dto);
        Task<PagingQueryResultModel<EmployeeEntity>> QueryWithSameDepartment(Guid accountId, EmployeeQueryDto dto);
        Task<IResultModel> SaveLatestSelect(Guid accountId, List<Guid> ids);
        Task<IResultModel> Update(EmployeeUpdateDto model);
        Task<IResultModel> UpdateAccount(EmployeeUpdateDto dto);
    }
}