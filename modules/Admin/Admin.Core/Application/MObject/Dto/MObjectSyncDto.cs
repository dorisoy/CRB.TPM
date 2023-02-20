using System;

namespace CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;

/// <summary>
/// 对象表，同步crm数据临时模型
/// </summary>

public class MObjectSyncDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 层级（10-雪花总部、20-事业部、30-营销中心、40-大区、50-业务部、60-工作站、70-客户）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 对象编码
    /// </summary>
    public string ObjectCode { get; set; }

    /// <summary>
    /// 对象名称
    /// </summary>
    public string ObjectName { get; set; }
    /// <summary>
    /// 总部Id
    /// </summary>
    public Guid HeadOfficeId { get; set; }

    /// <summary>
    /// 总部编码
    /// </summary>
    public string HeadOfficeCode { get; set; } = string.Empty;

    /// <summary>
    /// 总部名称
    /// </summary>
    public string HeadOfficeName { get; set; } = string.Empty;

    /// <summary>
    /// 事业部/区域Id
    /// </summary>
    public Guid DivisionId { get; set; }

    /// <summary>
    /// 事业部/区域编码
    /// </summary>
    public string DivisionCode { get; set; } = string.Empty;

    /// <summary>
    /// 事业部/区域名称
    /// </summary>
    public string DivisionName { get; set; } = string.Empty;
    /// <summary>
    /// 营销中心id
    /// </summary>
    public Guid MarketingId { get; set; }

    /// <summary>
    /// 营销中心编码
    /// </summary>
    public string MarketingCode { get; set; }

    /// <summary>
    /// 营销中心名称
    /// </summary>
    public string MarketingName { get; set; }

    /// <summary>
    /// 大区id
    /// </summary>
    public Guid BigAreaId { get; set; }

    /// <summary>
    /// 大区编码
    /// </summary>
    public string BigAreaCode { get; set; }

    /// <summary>
    /// 大区名称
    /// </summary>
    public string BigAreaName { get; set; }

    /// <summary>
    /// 业务部id
    /// </summary>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// 业务部编码
    /// </summary>
    public string OfficeCode { get; set; }

    /// <summary>
    /// 业务部名称
    /// </summary>
    public string OfficeName { get; set; }

    /// <summary>
    /// 工作站id
    /// </summary>
    public Guid StationId { get; set; }

    /// <summary>
    /// 工作站编码
    /// </summary>
    public string StationCode { get; set; }

    /// <summary>
    /// 工作站名称
    /// </summary>
    public string StationName { get; set; }

    /// <summary>
    /// 客户id
    /// </summary>
    public Guid DistributorId { get; set; }

    /// <summary>
    /// 客户编码
    /// </summary>
    public string DistributorCode { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    public string DistributorName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int Enabled { get; set; }

}