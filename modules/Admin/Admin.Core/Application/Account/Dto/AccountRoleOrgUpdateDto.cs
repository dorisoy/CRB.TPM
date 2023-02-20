using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Admin.Core.Application.Account.Dto;

[ObjectMap(typeof(AccountEntity), true)]
public class AccountRoleOrgUpdateDto 
{
    [Required(ErrorMessage = "请指定账户")]
    [GuidNotEmptyValidation(ErrorMessage = "请指定账户")]
    public Guid Id { get; set; }

    public IList<AccountRoleOrgUpdateVo> Datas { get; set; }
}

public class AccountRoleOrgUpdateVo
{
    /// <summary>
    /// 绑定角色列表
    /// </summary>
    [NotMappingColumn]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 雪花
    /// </summary>
    public List<Guid> HeadOffice { get; set; } = new();

    /// <summary>
    /// 事业部
    /// </summary>
    public List<Guid> Dbs { get; set; } = new();

    /// <summary>
    /// 营销中心
    /// </summary>
    public List<Guid> MarketingCenters { get; set; } = new();

    /// <summary>
    /// 大区
    /// </summary>
    public List<Guid> SaleRegions { get; set; } = new();

    /// <summary>
    /// 业务部
    /// </summary>
    public List<Guid> Departments { get; set; } = new();

    /// <summary>
    /// 工作站
    /// </summary>
    public List<Guid> Stations { get; set; } = new();
}



