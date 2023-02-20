using System;
using System.ComponentModel;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;

/// <summary>
/// 预警信息
/// </summary>
[EnableEntityAllEvent]
[Table("SYS_WarningInfo")]
public partial class WarningInfoEntity : Entity<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public WarningInfoType Type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Length(40)]
    [Nullable]
    public string OrgId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Length(100)]
    [Nullable]
    public string OrgCodeName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Length(4000)]
    [Nullable]
    public string Mess1 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Length(4000)]
    [Nullable]
    public string Mess2 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Length(4000)]
    [Nullable]
    public string Mess3 { get; set; }


    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.Now;
  
}

/// <summary>
/// 预警类型
/// </summary>
public enum WarningInfoType
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,
    /// <summary>
    /// 终端
    /// </summary>
    [Description("终端")]
    Terminal = 1,
    /// <summary>
    /// 经销商
    /// </summary>
    [Description("经销商")]
    Distributor = 2,
    /// <summary>
    /// 经销商终端关系
    /// </summary>
    [Description("经销商终端关系")]
    DistributorTerminal = 3,
    /// <summary>
    /// 人员终端关系
    /// </summary>
    [Description("人员终端关系")]
    UserTerminal = 4,
    /// <summary>
    /// 一二批商关系
    /// </summary>
    [Description("一二批商关系")]
    DistributorRelation = 5,
    /// <summary>
    /// 本渠道商主户头
    /// </summary>
    [Description("本渠道商主户头")]
    DistributorMainAccount = 6,
    /// <summary>
    /// 本渠道商主户头
    /// </summary>
    [Description("经销商业务实体")]
    DistributorEntity = 7
}