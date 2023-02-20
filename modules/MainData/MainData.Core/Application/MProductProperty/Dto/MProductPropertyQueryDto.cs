using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;

/// <summary>
/// 产品属性查询模型
/// </summary>
public class MProductPropertyQueryDto : QueryDto
{
    /// <summary>
    /// 类型
    /// </summary>
    public int ProductPropertiesType { get; set; }
    /// <summary>
    /// 编码/名称
    /// </summary>
    public string Name { get; set; }
}