using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect
{
    public interface IEmployeeLatestSelectService
    {
        Task<EmployeeLatestSelectEntity> Get(Guid employeeId, Guid relationId, IUnitOfWork uow);
        Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> Query(Guid employeeId, EmployeeLatestSelectQueryDto dto);
        Task<PagingQueryResultModel<EmployeeLatestSelectVo>> QueryView(Guid employeeId, EmployeeLatestSelectQueryDto dto);

    }
}