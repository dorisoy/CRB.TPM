using CRB.TPM.Data.Abstractions.Query;
using System;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
namespace CRB.TPM.Mod.Admin.Core.Application.Account.Dto;

public class AccountQueryDto : QueryDto
{
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
    /// 关键字
    /// </summary>
    public string Keys { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public AccountType? Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public AccountStatus? Status { get; set; }

    /// <summary>
    /// 雪花
    /// </summary>
    public Guid[] HeadOffice { get; set; } = new Guid[0];

    /// <summary>
    /// 事业部
    /// </summary>
    public Guid[] Dbs { get; set; } = new Guid[0];

    /// <summary>
    /// 营销中心
    /// </summary>
    public Guid[] MarketingCenters { get; set; } = new Guid[0];

    /// <summary>
    /// 大区
    /// </summary>
    public Guid[] SaleRegions { get; set; } = new Guid[0];

    /// <summary>
    /// 业务部
    /// </summary>
    public Guid[] Departments { get; set; } = new Guid[0];

    /// <summary>
    /// 工作站
    /// </summary>
    public Guid[] Stations { get; set; } = new Guid[0];

}


public class AccountSelectQueryDto : QueryDto
{
    public Guid[] Ids { get; set; }
    public AccountType? Type { get; set; }
}