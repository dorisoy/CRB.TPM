using System;
using System.ComponentModel;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure;

/// <summary>
/// 权限管理模块缓存键
/// </summary>
[SingletonInject]
public class AdminCacheKeys
{
    /// <summary>
    /// 验证码
    /// </summary>
    /// <param name="id">验证码ID</param>
    /// <returns></returns>
    public string VerifyCode(string id) => $"ADMIN:VERIFY_CODE:{id}";

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <param name="platform">平台</param>
    /// <returns></returns>
    public string RefreshToken(string refreshToken, int platform) => $"ADMIN:REFRESH_TOKEN:{platform}";

    /// <summary>
    /// 账户权限列表
    /// </summary>
    /// <param name="accountId">账户编号</param>
    /// <param name="platform">平台</param>
    /// <returns></returns>
    public string AccountPermissions(Guid accountId, int platform) => $"ADMIN:ACCOUNT:PERMISSIONS:{accountId}:{platform}";

    /// <summary>
    /// 字典下拉列表
    /// </summary>
    /// <param name="groupCode">字典分组编码</param>
    /// <param name="dictCode">字典编码</param>
    /// <returns></returns>
    public string DictSelect(string groupCode, string dictCode) => $"ADMIN:DICT:SELECT:{groupCode.ToUpper()}:{dictCode.ToUpper()}";

    /// <summary>
    /// 字典树
    /// </summary>
    /// <param name="groupCode"></param>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    public string DictTree(string groupCode, string dictCode) => $"ADMIN:DICT:TREE:{groupCode.ToUpper()}:{dictCode.ToUpper()}";

    /// <summary>
    /// 字典级联列表
    /// </summary>
    /// <param name="groupCode"></param>
    /// <param name="dictCode"></param>
    /// <returns></returns>
    public string DictCascader(string groupCode, string dictCode) => $"ADMIN:DICT:CASCADER:{groupCode.ToUpper()}:{dictCode.ToUpper()}";

    /// <summary>
    /// 账户页面列表
    /// <para>ADMIN:ACCOUNT:PAGES:账户编号</para>
    /// </summary>
    public  string AccountPages(Guid accountId, int platform) => $"ADMIN:ACCOUNT:PAGES:{accountId}:{platform}";

    /// <summary>
    /// 账户按钮列表
    /// <para>ADMIN:ACCOUNT:BUTTONS:账户编号</para>
    /// </summary>
    public  string AccountButtons(Guid accountId, int platform) => $"ADMIN:ACCOUNT:BUTTONS:{accountId}:{platform}";

    /// <summary>
    /// 账户菜单列表
    /// <para>ADMIN:ACCOUNT:MENUS :账户编号</para>
    /// </summary>
    public  string AccountMenus(Guid accountId, int platform) => $"ADMIN:ACCOUNT:MENUS:{accountId}:{platform}";

    /// <summary>
    /// 账户信息
    /// <para>ADMIN:ACCOUNT:INFO:账户编号</para>
    /// </summary>
    public  string Account(Guid accountId) => $"ADMIN:ACCOUNT:INFO:{accountId}";



    #region ORG

    /// <summary>
    /// 组织树
    /// </summary>
    public string MORGTree() => $"ADMIN:MORG_TREE";

    /// <summary>
    /// 组织树
    /// </summary>
    public string MORGTree(int? type) => $"ADMIN:MORG_TREE:{type}";

    /// <summary>
    /// 组织树
    /// </summary>
    public string MORGTree(int? type, Guid? guid) => $"ADMIN:MORG_TREE:{type}:{guid}";

    /// <summary>
    /// 解析当前访问用户的角色组织树
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public string CurrentAccountAROSTree(Guid accountId) => $"ADMIN:MORG_CURRENT_ACCOUNTAROSTREE:{accountId}";

    /// <summary>
    /// 对象
    /// </summary>
    public string MObject() => $"ADMIN:MOBJECT_QUERY";

    #endregion
}