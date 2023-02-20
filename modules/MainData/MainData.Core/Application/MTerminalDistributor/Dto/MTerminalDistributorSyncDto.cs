using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDistributor;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;

/// <summary>
/// 终端与经销商的关系同步临时表模型
/// </summary>
public class MTerminalDistributorSyncDto 
{
    /// <summary>
    ///  
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 终端编码
    /// </summary>
    public string TerminalCode { get; set; }

    /// <summary>
    /// 经销商编码
    /// </summary>
    public string DistributorCode { get; set; }

    /// <summary>
    /// 终端id
    /// </summary>
    public Guid? TerminalId { get; set; }

    /// <summary>
    /// 经销商id/分销商id
    /// </summary>
    public Guid? DistributorId { get; set; }

    /// <summary>
    /// 是否删除（同步CRM数据时使用） 是D的为删除
    /// </summary>
    public string UpdateMode { get; set; } = string.Empty;
}