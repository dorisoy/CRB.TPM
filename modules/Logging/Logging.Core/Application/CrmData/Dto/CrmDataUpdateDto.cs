
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;

/// <summary>
/// CRM客户、终端记录更新模型
/// </summary>
[ObjectMap(typeof(CrmDataEntity), true)]
public class CrmDataUpdateDto : CrmDataAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择CRM客户、终端记录")]
    [Required(ErrorMessage = "请选择要修改的CRM客户、终端记录")]
    public Guid Id { get; set; }
}