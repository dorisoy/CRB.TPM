using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;

/// <summary>
/// 营销中心产品添加模型
/// </summary>
[ObjectMap(typeof(MMarketingProductEntity))]

public class MMarketingProductAddDto
{
    /// <summary>
    /// 营销中心id
    /// </summary>
    public Guid MarketingId { get; set; }

    /// <summary>
    /// 产品id
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public bool Enabled { get; set; }

}