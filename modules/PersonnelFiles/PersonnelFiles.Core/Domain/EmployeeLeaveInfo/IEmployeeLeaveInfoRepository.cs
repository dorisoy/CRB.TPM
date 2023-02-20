using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Dto;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Domain.EmployeeLeaveInfo;

/// <summary>
/// 人员离职信息仓储
/// </summary>
public interface IEmployeeLeaveInfoRepository : IRepository<EmployeeLeaveInfoEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<EmployeeLeaveInfoEntity>> Query(EmployeeLeaveInfoQueryDto dto);

    /// <summary>
    /// 离职信息
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    Task<EmployeeLeaveInfoEntity> GetByEmployeeId(Guid employeeId);
}
