
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccount.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount
{
    /// <summary>
    /// 广告商银行账号表 M_ADBankAccount仓储
    /// </summary>
    public interface IMAdvertiserAccountRepository : IRepository<MAdvertiserAccountEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MAdvertiserAccountEntity>> Query(MAdvertiserAccountQueryDto dto);
    }
}


