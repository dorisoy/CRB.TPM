using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MMarketingSetup;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingSetup.Dto;

/// <summary>
/// 营销中心配置添加模型
/// </summary>
[ObjectMap(typeof(MMarketingSetupEntity))]
public class MMarketingSetupAddDto
{
    /// <summary>
    /// 营销中心id
    /// </summary>
    [Required]
    public Guid MarketingId { get; set; }

    /// <summary>
    /// 是否真实营销中心
    /// </summary>
    [Required]
    public bool IsReal { get; set; }

    /// <summary>
    /// 是否同步CRM组织
    /// </summary>
    [Required]
    public bool IsSynchronizeCRM { get; set; }

    /// <summary>
    /// 客户是否同步CRM工作站
    /// </summary>
    [Required]
    public bool IsSynchronizeCRMDistributorStation { get; set; }
}