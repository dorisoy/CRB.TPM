using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;

namespace CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;

/// <summary>
/// 经销商分销商关系关系同步临时表模型
/// </summary>
public class MDistributorRelationSyncDto
{
    /// <summary>
    ///  
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 经销商编码
    /// </summary>
    public string DistributorCode1 { get; set; }

    /// <summary>
    /// 分销商编码
    /// </summary>
    public string DistributorCode2 { get; set; }

    /// <summary>
    /// 经销商id
    /// </summary>
    public Guid? DistributorId1 { get; set; }

    /// <summary>
    /// 分销商id
    /// </summary>
    public Guid? DistributorId2 { get; set; }

    /// <summary>
    /// 是否删除（同步CRM数据时使用） 是D的为删除
    /// </summary>
    public string UpdateMode { get; set; } = string.Empty;

}