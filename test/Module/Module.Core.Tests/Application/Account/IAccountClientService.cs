using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Module.Core.Application.Account
{
    public interface IAccountClientService
    {
        Task<IResultModel> Add(AccountAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<IResultModel> Edit(Guid id);
        Task<IList<AccountEntity>> Query();
        Task<IResultModel> Update(AccountUpdateDto dto);
    }
}