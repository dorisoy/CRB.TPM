using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

namespace CRB.TPM.Mod.Module.Core.Application.Account.Dto;

[ObjectMap(typeof(AccountEntity), true)]
public class AccountUpdateDto : AccountAddDto
{
    [Required(ErrorMessage = "请选择账户")]
    [GuidNotEmptyValidation(ErrorMessage = "请选择账户")]
    public Guid Id { get; set; }
}