using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;

/// <summary>/// CRM的关系变动记录表添加模型
/// </summary>
[ObjectMap(typeof(CrmRelationEntity))]
public class CrmRelationAddDto
{
    /// <summary>
    /// 编码1
    /// </summary>
    public string Code1 { get; set; }

    /// <summary>
    /// 编码2
    /// </summary>
    public string Code2 { get; set; }

    /// <summary>
    /// 关系类型
    /// </summary>
    public string RELTYP { get; set; }

    /// <summary>
    /// 操作类型，D是删除；其他都可看作新增
    /// </summary>
    public string ZUPDMODE { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ZDATE { get; set; }

}