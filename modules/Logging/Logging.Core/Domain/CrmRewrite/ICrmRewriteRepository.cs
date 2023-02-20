
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;

namespace CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite
{
    /// <summary>
    /// 返写CRM记录仓储
    /// </summary>
    public interface ICrmRewriteRepository : IRepository<CrmRewriteEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<CrmRewriteEntity>> Query(CrmRewriteQueryDto dto);
    }
}


