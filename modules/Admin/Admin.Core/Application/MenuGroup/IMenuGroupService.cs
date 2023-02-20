using System;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MenuGroup.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MenuGroup;

namespace CRB.TPM.Mod.Admin.Core.Application.MenuGroup;

/// <summary>
/// 菜单分组服务接口
/// </summary>
public interface IMenuGroupService
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<PagingQueryResultModel<MenuGroupEntity>> Query(MenuGroupQueryDto dto);

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(MenuGroupAddDto dto);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Update(MenuGroupUpdateDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 查询菜单分组下拉选项
    /// </summary>
    /// <returns></returns>
    Task<IResultModel> Select();
}