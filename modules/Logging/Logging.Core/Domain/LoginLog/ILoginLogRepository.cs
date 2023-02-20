using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;
using System.Threading.Tasks;


namespace CRB.TPM.Mod.Logging.Core.Domain.LoginLog
{
    /// <summary>
    /// 登录日志仓储
    /// </summary>
    public interface ILoginLogRepository : IRepository<LoginLogEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingQueryResultModel<LoginLogEntity>> Query(LoginLogQueryDto dto);
    }
}
