
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDetail.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail
{
    /// <summary>
    /// 终端其他信息仓储
    /// </summary>
    public interface IMTerminalDetailRepository : IRepository<MTerminalDetailEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MTerminalDetailEntity>> Query(MTerminalDetailQueryDto dto);
    }
}


