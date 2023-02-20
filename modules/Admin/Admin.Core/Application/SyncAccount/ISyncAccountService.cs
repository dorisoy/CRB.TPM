using CRB.TPM.Mod.Admin.Core.Application.SyncAccount.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Application.SyncAccount
{
    public interface ISyncAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="userRelData"></param>
        /// <returns></returns>
        Task<IResultModel> SyncData(List<ET_ORG> userData, List<ET_BPREL> userRelData);
        //Task<bool> UpdateSysAccount(List<AccountSyncDto> accountData);
    }
}
