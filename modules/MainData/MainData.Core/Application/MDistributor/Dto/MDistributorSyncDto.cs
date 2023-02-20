using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

/// <summary>
/// 经销商同步写临时表模型
/// </summary>
public class MDistributorSyncDto
{
    /// <summary>
    ///  
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string DistributorCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string DistributorName { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int DistributorType { get; set; }

    /// <summary>
    /// 工作站id
    /// </summary>
    public Guid StationId { get; set; }

    /// <summary>
    /// 状态 0 失效；1有效；2冻结
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// CRM编码
    /// </summary>
    public string CrmCode { get; set; }

    /// <summary>
    /// 1表示主户；2管理开户的子户；3TPM虚拟子户
    /// </summary>
    public int DetailType { get; set; }

    /// <summary>
    /// 经销商编码
    /// </summary>
    public string CustomerCode { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Creator { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// CRM数据
    /// </summary>
    public string JsonString { get; set; }
    /// <summary>
    /// CRM时间
    /// </summary>
    public DateTime ZDATE { get; set; }
    /// <summary>
    /// 业务实体ID
    /// </summary>
    public Guid EntityId { get; set; }
}