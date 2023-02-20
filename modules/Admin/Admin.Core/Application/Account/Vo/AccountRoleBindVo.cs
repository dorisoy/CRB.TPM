using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.Account.Vo;

/// <summary>
/// 账户角色绑定模型
/// </summary>
public class AccountRoleBindVo
{
    [Required(ErrorMessage = "请选择账户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    [Required(ErrorMessage = "请选择角色")]
    public int RoleId { get; set; }

    /// <summary>
    /// 是否选中
    /// </summary>
    public bool Checked { get; set; }
}
