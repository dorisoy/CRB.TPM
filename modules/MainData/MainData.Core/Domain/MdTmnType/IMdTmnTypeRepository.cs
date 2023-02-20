
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;
using CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdTmnType
{
    /// <summary>
    /// 终端类型（一二三级） M_TmnType仓储
    /// </summary>
    public interface IMdTmnTypeRepository : IRepository<MdTmnTypeEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdTmnTypeEntity>> Query(MdTmnTypeQueryDto dto);
    }
}


