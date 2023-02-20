
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;

/// <summary>
/// 返写CRM记录更新模型
/// </summary>
[ObjectMap(typeof(CrmRewriteEntity), true)]
public class CrmRewriteUpdateDto : CrmRewriteAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择返写CRM记录")]
    [Required(ErrorMessage = "请选择要修改的返写CRM记录")]
    public Guid Id { get; set; }
}