using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;

/// <summary>
/// 最近选择
/// </summary>
public interface IEmployeeLatestSelectRepository : IRepository<EmployeeLatestSelectEntity>
{

    /// <summary>
    /// 查询最近列表
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> Query(Guid employeeId, EmployeeLatestSelectQueryDto model);

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="relationId"></param>
    /// <param name="uow"></param>
    /// <returns></returns>
    Task<EmployeeLatestSelectEntity> Get(Guid employeeId, Guid relationId, IUnitOfWork uow);


    Task<PagingQueryResultModel<EmployeeLatestSelectVo>> QueryView(Guid employeeId, EmployeeLatestSelectQueryDto dto);

}
