using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;

namespace CRB.TPM.Mod.Admin.Core.Application.Role.Dto;

/// <summary>
/// 角色绑定菜单更新
/// </summary>
public class RoleBindMenusUpdateDto
{
    /// <summary>
    /// 角色编号
    /// </summary>
    [Required(ErrorMessage = "请选择角色")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 绑定的菜单列表
    /// </summary>
    public IList<BindMenuUpdateDto> Menus { get; set; }
}

public class BindMenuUpdateDto
{
    /// <summary>
    /// 菜单编码
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType MenuType { get; set; }

    /// <summary>
    /// 按钮编码列表
    /// </summary>
    public IList<string> Buttons { get; set; }

    /// <summary>
    /// 绑定的权限编码列表
    /// </summary>
    public IList<string> Permissions { get; set; }
}