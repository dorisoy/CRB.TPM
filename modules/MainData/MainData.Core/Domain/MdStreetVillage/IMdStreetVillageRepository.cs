
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;
using CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage
{
    /// <summary>
    /// 街道村 D_StreetVillage仓储
    /// </summary>
    public interface IMdStreetVillageRepository : IRepository<MdStreetVillageEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdStreetVillageEntity>> Query(MdStreetVillageQueryDto dto);
    }
}


