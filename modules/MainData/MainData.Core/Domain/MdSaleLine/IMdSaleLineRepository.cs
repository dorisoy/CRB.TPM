
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;
using CRB.TPM.Mod.MainData.Core.Application.MdSaleLine.Dto;

namespace CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine
{
    /// <summary>
    /// 业务线 D_SaleLine仓储
    /// </summary>
    public interface IMdSaleLineRepository : IRepository<MdSaleLineEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<MdSaleLineEntity>> Query(MdSaleLineQueryDto dto);
    }
}


