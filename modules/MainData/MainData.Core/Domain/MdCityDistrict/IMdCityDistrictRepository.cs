
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;
using CRB.TPM.Mod.MainData.Core.Application.MdCityDistrict.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict
{
    /// <summary>
    /// 城市区县 D_CityDistrict仓储
    /// </summary>
    public interface IMdCityDistrictRepository : IRepository<MdCityDistrictEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdCityDistrictEntity>> Query(MdCityDistrictQueryDto dto);
    }
}


