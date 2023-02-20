
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg
{
    /// <summary>
    /// 广告商营销组织关系表 M_Re_Org_AD仓储
    /// </summary>
    public interface IMAdvertiserOrgRepository : IRepository<MAdvertiserOrgEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MAdvertiserOrgEntity>> Query(MAdvertiserOrgQueryDto dto);
    }
}


