
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity;
using CRB.TPM.Mod.MainData.Core.Application.MdProvinceCity.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdProvinceCity
{
    /// <summary>
    /// 省份城市 D_ProvinceCity仓储
    /// </summary>
    public interface IMdProvinceCityRepository : IRepository<MdProvinceCityEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdProvinceCityEntity>> Query(MdProvinceCityQueryDto dto);
    }
}


