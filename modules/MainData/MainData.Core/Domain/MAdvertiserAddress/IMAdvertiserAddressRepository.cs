
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress
{
    /// <summary>
    /// 广告商地点表 M_ADAddress仓储
    /// </summary>
    public interface IMAdvertiserAddressRepository : IRepository<MAdvertiserAddressEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MAdvertiserAddressEntity>> Query(MAdvertiserAddressQueryDto dto);
    }
}


