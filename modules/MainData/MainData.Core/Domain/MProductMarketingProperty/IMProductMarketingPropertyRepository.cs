
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;
using CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty
{
    /// <summary>
    /// 营销产品属性仓储
    /// </summary>
    public interface IMProductMarketingPropertyRepository : IRepository<MProductMarketingPropertyEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MProductMarketingPropertyEntity>> Query(MProductMarketingPropertyQueryDto dto);
    }
}


