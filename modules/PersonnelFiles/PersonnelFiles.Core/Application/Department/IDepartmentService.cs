using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Department;

/// <summary>
/// 用于表示部门服务
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(DepartmentAddDto dto);

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 编辑部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 获取部门树
    /// </summary>
    /// <returns></returns>
    Task<IResultModel> GetTree();

    /// <summary>
    /// 分页查询部门
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<DepartmentEntity>> Query(DepartmentQueryDto dto);

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IResultModel> Update(DepartmentUpdateDto model);

    /// <summary>
    /// 更新排序
    /// </summary>
    /// <param name="departments"></param>
    /// <returns></returns>
    Task<IResultModel> UpdateSort(IList<DepartmentEntity> departments);
}