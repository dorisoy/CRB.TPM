
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;
using CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet
{
    /// <summary>
    /// 区县街道 D_DistrictStreet仓储
    /// </summary>
    public interface IMdDistrictStreetRepository : IRepository<MdDistrictStreetEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdDistrictStreetEntity>> Query(MdDistrictStreetQueryDto dto);
    }
}


