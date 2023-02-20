
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;

/// <summary>
/// KA大系统 M_KABigSysNameConf更新模型
/// </summary>
[ObjectMap(typeof(MdKaBigSysNameConfEntity), true)]
public class MdKaBigSysNameConfUpdateDto : MdKaBigSysNameConfAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择KA大系统 M_KABigSysNameConf")]
    [Required(ErrorMessage = "请选择要修改的KA大系统 M_KABigSysNameConf")]
    public Guid Id { get; set; }
}