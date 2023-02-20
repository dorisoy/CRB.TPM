
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser
{
    /// <summary>
    /// 广告商仓储
    /// </summary>
    public interface IMAdvertiserRepository : IRepository<MAdvertiserEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MAdvertiserEntity>> Query(MAdvertiserQueryDto dto);
    }
}


