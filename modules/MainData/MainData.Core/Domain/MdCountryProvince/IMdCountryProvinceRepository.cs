
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;
using CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince
{
    /// <summary>
    /// 国家省份 D_CountryProvince仓储
    /// </summary>
    public interface IMdCountryProvinceRepository : IRepository<MdCountryProvinceEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdCountryProvinceEntity>> Query(MdCountryProvinceQueryDto dto);
    }
}


