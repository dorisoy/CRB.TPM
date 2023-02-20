
using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccount.Dto;

/// <summary>
/// 广告商银行账号表 M_ADBankAccount更新模型
/// </summary>
[ObjectMap(typeof(MAdvertiserAccountEntity), true)]
public class MAdvertiserAccountUpdateDto : MAdvertiserAccountAddDto
{
    [GuidNotEmptyValidation(ErrorMessage = "请选择广告商银行账号表 M_ADBankAccount")]
    [Required(ErrorMessage = "请选择要修改的广告商银行账号表 M_ADBankAccount")]
    public Guid Id { get; set; }
}