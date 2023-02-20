using System;
using System.Collections.Generic;
using CRB.TPM.Data.Abstractions.Annotations;
using System.Dynamic;
using CRB.TPM.Utils.Annotations;
using System.Text.Json.Serialization;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;


namespace CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;

/// <summary>
/// 个人信息
/// </summary>
public class ProfileVo
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public int Platform { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 皮肤设置
    /// </summary>
    public ProfileSkinVo Skin { get; set; }

    /// <summary>
    /// 标签导航
    /// </summary>
    public TabnavVo Tabnav { get; set; } = new();

    /// <summary>
    /// 菜单列表
    /// </summary>
    public IList<ProfileMenuVo> Menus { get; set; }

    /// <summary>
    /// 按钮编码列表
    /// </summary>
    public IList<string> Buttons { get; set; } = new List<string>();

    /// <summary>
    /// 详情信息(用于扩展登录对象信息)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Details { get; set; }

    /// <summary>
    /// 自定义样式
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string CustomCss { get; set; }

    /// <summary>
    /// 关联角色
    /// </summary>
    public List<OptionResultModel> Roles { get; set; } = new();

    /// <summary>
    /// 关联用户角色
    /// </summary>
    public List<ExpandoObject> AccountRoles { get; set; } = new();

    /// <summary>
    /// 关联用户角色组织
    /// </summary>
    public List<ExpandoObject> AccountRoleOrgs { get; set; } = new();

    /// <summary>
    /// 关联用户角色组织
    /// </summary>
    public List<AROVo> AROS { get; set; } = new();

    /// <summary>
    /// 当前访问用户的角色组织树（合并用户角色）
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TreeResultModel<Guid, MOrgTreeVo>> AROSTree = new();


    /// <summary>
    /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    public MOrgLevelVo MOrgs { get; set; } = new();

}

/// <summary>
/// 账户组织关系
/// </summary>
public class AROVo
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public List<string> Orgs { get; set; } = new();
}
