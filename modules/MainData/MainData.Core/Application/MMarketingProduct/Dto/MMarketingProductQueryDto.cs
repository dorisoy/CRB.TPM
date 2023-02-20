using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using System.Collections.Generic;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;

/// <summary>
/// 营销中心产品查询模型
/// </summary>
public class MMarketingProductQueryDto : GlobalOrgFilterDto
{
    /// <summary>
    /// 营销中心id
    /// </summary>
    public Guid MarketingId { get; set; }
    /// <summary>
    /// 产品id
    /// </summary>
    public Guid ProductId { get; set; }
}