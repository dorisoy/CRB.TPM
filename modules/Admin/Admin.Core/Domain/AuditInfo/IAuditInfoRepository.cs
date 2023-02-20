using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.AuditInfo.Dto;
using CRB.TPM.Utils.Models;

using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using CRB.TPM.Mod.Admin.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;
using CRB.TPM.Module.Abstractions;

namespace CRB.TPM.Mod.Admin.Core.Domain.AuditInfo
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
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        Task<AuditInfoEntity> Details(int id, IList<ModuleDescriptor> modules = null);

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
