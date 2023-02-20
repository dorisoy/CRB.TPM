
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;
using CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;

namespace CRB.TPM.Mod.Logging.Core.Domain.CrmData
{
    /// <summary>
    /// CRM客户、终端记录仓储
    /// </summary>
    public interface ICrmDataRepository : IRepository<CrmDataEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<CrmDataEntity>> Query(CrmDataQueryDto dto);
    }
}


