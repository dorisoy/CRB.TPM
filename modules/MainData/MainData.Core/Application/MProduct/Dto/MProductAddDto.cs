using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Utils.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto;

/// <summary>
/// 添加模型
/// </summary>
[ObjectMap(typeof(MProductEntity))]
public class MProductAddDto
{
    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(50, ErrorMessage = "产品编码最长50位")]
    public string ProductCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(200, ErrorMessage = "产品名称最长200位")]
    public string ProductName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int BottleBox { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// 类别名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "类别名称最长20位")]
    public string ClassName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string VolumeName { get; set; }

    /// <summary>
    /// 品牌名称
    /// </summary>
    [MaxLength(20, ErrorMessage = "品牌名称最长20位")]
    public string BrandName { get; set; }

    /// <summary>
    /// 外包装
    /// </summary>
    [MaxLength(20, ErrorMessage = "外包装最长20位")]
    public string OutPackName { get; set; }

    /// <summary>
    ///  内包装
    /// </summary>
    [MaxLength(20, ErrorMessage = "内包装最长20位")]
    public string InPackName { get; set; }

    /// <summary>
    /// 1（17位码）；2（11位码）；3（促销品）……
    /// </summary>
    public int ProductType { get; set; }

    /// <summary>
    /// 产品规格
    /// </summary>
    [MaxLength(50, ErrorMessage = "内包装最长50位")]
    public string ProductSpecName { get; set; }

    /// <summary>
    /// 升转换
    /// </summary>
    public decimal LitreConversionRate { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 17位对应的11位码产品id
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 集团码
    /// </summary>
    [MaxLength(20, ErrorMessage = "集团码最长20位")]
    public string GroupCode { get; set; }

    /// <summary>
    /// 集团名称
    /// </summary>
    [MaxLength(200, ErrorMessage = "集团名称最长200位")]
    public string GroupName { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 特征码
    /// </summary>
    [MaxLength(10, ErrorMessage = "特征码最长10位")]
    public string CharacterCode { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(500, ErrorMessage = "备注最长500位")]
    public string Remark { get; set; }

}