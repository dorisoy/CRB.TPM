using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.Logging.Core.Domain.CrmData;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;

/// <summary>
/// CRM客户、终端记录添加模型
/// </summary>
[ObjectMap(typeof(CrmDataEntity))]
public class CrmDataAddDto
{
    /// <summary>
    /// 数据类型，7-客户；8-终端
    /// </summary>
    public int DataType { get; set; }

    /// <summary>
    /// 客户编码、终端编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// json值
    /// </summary>
    public string JsonString { get; set; }

    /// <summary>
    /// crm时间
    /// </summary>
    public DateTime ZDATE { get; set; }

}