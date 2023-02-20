
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;

namespace CRB.TPM.Mod.Logging.Core.Domain.CrmRelation
{
    /// <summary>
    /// CRM的关系变动记录表仓储
    /// </summary>
    public interface ICrmRelationRepository : IRepository<CrmRelationEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<CrmRelationEntity>> Query(CrmRelationQueryDto dto);
    }
}


