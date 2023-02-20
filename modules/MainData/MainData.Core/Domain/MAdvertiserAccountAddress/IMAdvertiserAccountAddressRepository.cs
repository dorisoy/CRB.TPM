
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress
{
    /// <summary>
    /// 广告商地点分配表 M_Re_ADAddressAccount仓储
    /// </summary>
    public interface IMAdvertiserAccountAddressRepository : IRepository<MAdvertiserAccountAddressEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MAdvertiserAccountAddressEntity>> Query(MAdvertiserAccountAddressQueryDto dto);
    }
}


