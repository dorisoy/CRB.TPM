
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;

namespace CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;

/// <summary>
/// 业务实体更新模型
/// </summary>
[ObjectMap(typeof(MEntityEntity), true)]
public class MEntityUpdateDto : MEntityAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择业务实体")]
    [Required(ErrorMessage = "请选择要修改的业务实体")]
    public Guid Id { get; set; }
}