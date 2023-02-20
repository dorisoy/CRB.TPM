
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;

/// <summary>
/// CRM的关系变动记录表更新模型
/// </summary>
[ObjectMap(typeof(CrmRelationEntity), true)]
public class CrmRelationUpdateDto : CrmRelationAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择CRM的关系变动记录表")]
    [Required(ErrorMessage = "请选择要修改的CRM的关系变动记录表")]
    public Guid Id { get; set; }
}