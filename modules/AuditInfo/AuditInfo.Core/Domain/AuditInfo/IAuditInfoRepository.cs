using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo.Dto;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Utils.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Mod.Admin.Core.Domain.Account;

namespace CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo
{
    /// <summary>
    /// 审计信息仓储
    /// </summary>
    public interface IAuditInfoRepository : IRepository<AuditInfoEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<AuditInfoEntity>> Query(AuditInfoQueryDto dto);

        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modules"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        Task<AuditInfoEntity> Details(int id, IList<ModuleDescriptor> modules = null, IList<AccountEntity> accounts = null);

        /// <summary>
        /// 查询最近一周访问量
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPv();

        /// <summary>
        /// 按照模块查询访问量
        /// </summary>
        /// <returns></returns>
        Task<IList<OptionResultModel>> QueryCountByModule();
    }
}
