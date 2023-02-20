using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace CRB.TPM.Mod.Admin.Core.Domain.Account;



public partial class AccountEntity
{
    /// <summary>
    /// 租户名称
    /// </summary>
    [NotMappingColumn]
    public string TenantName { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [NotMappingColumn]
    public string RoleName { get; set; }

    /// <summary>
    /// 关联角色
    /// </summary>
    [NotMappingColumn]
    public List<OptionResultModel> Roles { get; set; } = new();

    /// <summary>
    /// 关联用户角色
    /// </summary>

    [NotMappingColumn]
    //public List<(string OrgId, Guid Id)> AccountRoles { get; set; }
    //public List<Record<Guid, Guid>> AccountRoles { get; set; }
    public List<ExpandoObject> AccountRoles { get; set; }

    /// <summary>
    /// 关联用户角色组织
    /// </summary>
    [NotMappingColumn]
    //public List<(string OrgId, Guid Id)> AccountRoleOrgs { get; set; }
    //public List<Record2<string, Guid>> AccountRoleOrgs { get; set; }
    public List<ExpandoObject> AccountRoleOrgs { get; set; }

    [NotMappingColumn]
    public List<ExpandoObject> AROS { get; set; }


    /// <summary>
    /// 当前访问用户的角色拥有的组织层级（合并用户角色和组织）
    /// </summary>
    [NotMappingColumn]
    public MOrgLevelVo MOrgs { get; set; } = new();


    /// <summary>
    /// 账户类型名称
    /// </summary>
    [NotMappingColumn]
    public string TypeName => Type.ToDescription();


    /// <summary>
    /// 账户检测
    /// </summary>
    public IResultModel Check()
    {
        if (Deleted || Status == AccountStatus.Logout)
            return ResultModel.Failed("账户不存在");

        if (Status == AccountStatus.Register)
            return ResultModel.Failed("账户未激活");

        if (Status == AccountStatus.Disabled)
            return ResultModel.Failed("账户已禁用，请联系管理员");

        return ResultModel.Success();
    }

    [NotMappingColumn]
    public bool IsAdmin
    {
        get 
        {
            var name = this.Name.ToUpper();
            var username = this.Username.ToUpper();
            return name.Equals("ADMIN") || name.Equals("超级管理员") || name.Equals("ADMINISTRATOR") || username.Equals("ADMIN") || username.Equals("超级管理员") || username.Equals("ADMINISTRATOR");
        }
    }
}
