using System.ComponentModel;


namespace CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;


/// <summary>
/// 用于表述主数据组织架构层级
/// </summary>
public enum OrgEnumType : int
{
    /// <summary>
    /// 雪花
    /// </summary>
    [Description("雪花")]
    HeadOffice = 10,

    /// <summary>
    /// 事业部
    /// </summary>
    [Description("事业部")]
    BD = 20,

    /// <summary>
    /// 营销中心
    /// </summary>
    [Description("营销中心")]
    MarketingCenter = 30,

    /// <summary>
    /// 大区
    /// </summary>
    [Description("大区")]
    SaleRegion = 40,

    /// <summary>
    /// 业务部
    /// </summary>
    [Description("业务部")]
    Department = 50,

    /// <summary>
    /// 工作站
    /// </summary>
    [Description("工作站")]
    Station = 60,

    /// <summary>
    /// 客户
    /// </summary>
    [Description("客户")]
    Distributor = 70,
}