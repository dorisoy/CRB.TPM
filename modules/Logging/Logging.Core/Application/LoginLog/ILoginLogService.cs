using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Application.LoginLog;

public interface ILoginLogService
{
    Task<IResultModel> Add(LoginLogAddDto dto);

    Task<IResultModel> Delete(int id);

    Task<PagingQueryResultModel<LoginLogEntity>> Query(LoginLogQueryDto dto);

    /// <summary>
    /// 导出登录日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IResultModel<ExcelModel>> ExportLogin(LoginLogQueryDto dto);
}