using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;

/// <summary>
/// 查询模型
/// </summary>
public class MProductQueryDto : QueryDto
{
    /// <summary>
    /// 产品编码/名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 产品类型
    /// </summary>
    public int ProductType { get; set; }
}