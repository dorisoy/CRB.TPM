using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure;

/// <summary>
/// 账户资料解析器
/// </summary>
public interface IAccountResolver
{
    /// <summary>
    /// 当前账户
    /// </summary>
    IAccount CurrentAccount { get; }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="account">账户信息</param>
    /// <param name="platform">登录平台</param>
    /// <returns></returns>
    Task<ProfileVo> Resolve(AccountEntity account, int platform);

    /// <summary>
    /// 获取当前用户AROS(账户角色组织关系：数据权限)
    /// </summary>
    List<AROVo> AROS { get; }

    /// <summary>
    ///  获取当前访问用户的角色拥有的组织（合并用户角色和组织）
    /// </summary>
    List<Guid> CurrentAROS { get; }

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    Task<MOrgLevelVo> CurrentMOrg(List<string> maros = null);

    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    /// <returns></returns>
    //Task<List<MOrgLevelTreeVo>> CurrentMOrgs();
    IAsyncEnumerable<MOrgLevelTreeVo> CurrentMOrgs();

    /// <summary>
    ///  解析当前访问用户的角色组织树（合并用户角色）
    /// </summary>
    /// <returns></returns>
    [Obsolete("该方法已经弃用，请使用 CurrentMOrgs 替代")]
    Task<List<TreeResultModel<Guid, MOrgTreeVo>>> CurrentAccountAROSTree();

    /// <summary>
    /// 根据组织权限生成Sql条件字符串
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="orgLevel"></param>
    /// <param name="objAlias"></param>
    /// <returns></returns>
    Task<string> BuildSqlWhereStrByOrgAuth(GlobalOrgFilterDto dto, OrgEnumType orgLevel = OrgEnumType.Station, string objAlias = "mobj");

    ///// <summary>
    ///// 给指定对象绑定组织权限方法
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="obj"></param>
    //void BindOrgAuthPropFunc<T>(T obj);
    /// <summary>
    /// 检查传入的组织id是否有权限
    /// </summary>
    /// <param name="orgType"></param>
    /// <param name="orgIds"></param>
    /// <returns></returns>
    Task<(bool isAuth, IList<string> noAuthCode)> CheckOrgIdsAuth(OrgEnumType orgType, IList<Guid> orgIds);
}