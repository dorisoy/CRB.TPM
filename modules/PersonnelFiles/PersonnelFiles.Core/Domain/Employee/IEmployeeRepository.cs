using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using CRB.TPM.Mod.Admin.Core.Domain.Account;

namespace CRB.TPM.Mod.PS.Core.Domain.Employee;

/// <summary>
/// 人员信息仓储
/// </summary>
public interface IEmployeeRepository : IRepository<EmployeeEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<EmployeeEntity>> Query(EmployeeQueryDto dto, IList<AccountEntity> accounts);

    /// <summary>
    /// 判断是否有人员绑定了指定部门
    /// </summary>
    /// <param name="departmentId"></param>
    /// <returns></returns>
    Task<bool> ExistsBindDept(Guid departmentId);

    /// <summary>
    /// 判断是否有人员绑定了指定岗位
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<bool> ExistsBindPost(Guid postId);

    /// <summary>
    /// 修改离职状态
    /// </summary>
    /// <param name="id">人员编号</param>
    /// <param name="uow"></param>
    /// <returns></returns>
    Task<bool> UpdateLeaveStatus(Guid id, IUnitOfWork uow);

    /// <summary>
    /// 根据账户编号查询人员信息
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task<EmployeeEntity> GetByAccountId(Guid accountId);

    /// <summary>
    /// 查询指定部门下的所有人员列表
    /// </summary>
    /// <param name="departmentId">部门编号</param>
    /// <returns></returns>
    Task<IList<EmployeeEntity>> QueryByDepartment(Guid departmentId);

    /// <summary>
    /// 查询包含扩展属性的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<EmployeeEntity> GetWidthExtend(Guid id);
}
