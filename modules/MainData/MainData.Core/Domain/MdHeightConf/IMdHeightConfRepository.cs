
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;
using CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf
{
    /// <summary>
    /// 制高点配置 M_HeightConf仓储
    /// </summary>
    public interface IMdHeightConfRepository : IRepository<MdHeightConfEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdHeightConfEntity>> Query(MdHeightConfQueryDto dto);
    }
}


