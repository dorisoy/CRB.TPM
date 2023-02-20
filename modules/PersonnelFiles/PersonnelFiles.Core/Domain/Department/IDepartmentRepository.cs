using CRB.TPM.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Data.Abstractions.Query;

namespace CRB.TPM.Mod.PS.Core.Domain.Department;

/// <summary>
/// 部门仓储
/// </summary>
public interface IDepartmentRepository : IRepository<DepartmentEntity>
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<DepartmentEntity>> Query(DepartmentQueryDto dto, IList<AccountEntity> accounts);

    /// <summary>
    /// 同一层级中名称是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsName(string name, Guid parentId, Guid? id = null);

    /// <summary>
    /// 编码是否存在
    /// </summary>
    /// <param name="code"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsCode(string code, Guid? id = null);

    /// <summary>
    /// 判断是否存在子部门
    /// </summary>
    /// <param name="parentId"></param>
    /// <returns></returns>
    Task<bool> ExistsChildren(Guid parentId);
}
