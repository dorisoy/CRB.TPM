using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

/// <summary>/// 经销商/分销商添加模型
/// </summary>
[ObjectMap(typeof(MDistributorEntity))]
public class MDistributorAddDto
{
    /// <summary>
    /// 客户编码
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "客户编码最长30位")]
    public string DistributorCode { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    [Required]
    [MaxLength(60, ErrorMessage = "客户名称最长60位")]
    public string DistributorName { get; set; }

    /// <summary>
    /// 类型：1经销商、2分销商
    /// </summary>
    [Required]
    public int DistributorType { get; set; }

    /// <summary>
    /// 工作站id
    /// </summary>
    [Required]
    public Guid StationId { get; set; }

    /// <summary>
    /// 业务实体id
    /// </summary>
    [Required]
    public Guid EntityId { get; set; }

    /// <summary>
    /// CRM编码
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "CRM编码最长30位")]
    public string CrmCode { get; set; }

    /// <summary>
    /// 1表示主户；2管理开户的子户；3TPM虚拟子户
    /// </summary>
    [Required]
    public int DetailType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 经销商编码 用于经销商分析。主户填其自身编码；虚拟子户、管理开户的填主客户编码。
    /// </summary>
    [Required]
    [MaxLength(30, ErrorMessage = "经销商编码最长30位")]
    public string CustomerCode { get; set; }

    /// <summary>
    /// 子户父亲
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 是否跟CRM变动工作站
    /// </summary>
    public int IsSynchronizeCRMStation { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public Guid CreatedBy { get; set; }
}