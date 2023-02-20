using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.MOrg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg;

/// <summary>
/// 组织表服务
/// </summary>
public interface IMOrgService
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
   Task<PagingQueryResultModel<MOrgEntity>> Query(MOrgQueryDto dto);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Add(MOrgAddDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">编号</param>
    /// <returns></returns>
    Task<IResultModel> Delete(Guid id);

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResultModel> Edit(Guid id);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel> Update(MOrgUpdateDto dto);

    /// <summary>
    /// 获取指定层级组织
    /// </summary>
    /// <param name="level"></param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    Task<IResultModel> GetOrgLevel(int? level = 1, bool ignore = false);

    /// <summary>
    /// 根据父ID获取组织节点
    /// </summary>
    /// <param name="level"></param>
    /// <param name="parentId"></param>
    /// <param name="ignore"></param>
    /// <returns></returns>
    Task<IResultModel> GetNodeByParentId(int? level = 10, Guid? parentId = null, bool ignore = false);

    /// <summary>
    /// 获取组织树
    /// </summary>
    /// <param name="level">表示指定获取几个层级的数据,默认1层</param>
    /// <param name="metadata">是否显示元数据</param>
    /// <returns></returns>
    Task<IResultModel> GetTree(int? level = 1, bool metadata = false);

    /// <summary>
    /// 根据父级ID获取组织树
    /// </summary>
    /// <param name="level">表示指定获取几个层级的数据,默认1层</param>
    /// <param name="parentId">组织父级ID</param>
    /// <param name="metadata">是否显示元数据</param>
    /// <returns></returns>
    Task<IResultModel> GetTreeByParentId(int? level = 1, Guid? parentId = null, bool metadata = false);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IResultModel> DeleteSelected(IEnumerable<Guid> ids);

    /// <summary>
    /// 组织联动组件
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<PagingQueryResultModel<OrgSelectVo>> Select(OrgSelectDto dto);

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    Task<MOrgLevelVo> CurrentMOrg();

    ///// <summary>
    ///// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    ///// </summary>
    ///// <returns></returns>
    //Task<List<MOrgLevelTreeVo>> CurrentMOrgs();

    /// <summary>
    /// 从获取组织数据
    /// </summary>
    /// <returns></returns>
    Task<IList<MOrgEntity>> GetForCache();
}
