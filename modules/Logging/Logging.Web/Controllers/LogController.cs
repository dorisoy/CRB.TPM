using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog;
using CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Web.Controllers;

[OpenApiTag("Log", AddToDocument = true, Description = "系统日志管理")]
public class LogController : Web.ModuleController
{
    private readonly ILoginLogService _service;
    private readonly IPlatformProvider _platformProvider;
    public LogController(ILoginLogService service, IPlatformProvider platformProvider)
    {
        _service = service;
        _platformProvider = platformProvider;
    }

    /// <summary>
    /// 查询日志
    /// </summary>
    [HttpGet]
    [DisableAudit]
    public async Task<PagingQueryResultModel<LoginLogEntity>> Query([FromQuery] LoginLogQueryDto dto)
    {
        var result = await _service.Query(dto);
        if (result.Data != null && result.Data.Rows.Any())
        {
            foreach (var r in result.Data.Rows)
            {
                r.PlatformName = _platformProvider.ToDescription(r?.Platform ?? -1);
            }
        }
        return result;
    }

    /// <summary>
    /// 添加日志
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    [DisableAudit]
    public async Task<IResultModel> Add(LoginLogAddDto dto)
    {
        return await _service.Add(dto);
    }

    /// <summary>
    /// 删除日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [DisableAudit]
    public async Task<IResultModel> Delete([BindRequired] int id)
    {
        return  await _service.Delete(id);
    }

    /// <summary>
    /// 导出登录日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [DisableAudit]
    [Description("导出登录日志")]
    public async Task<IActionResult> LoginExport(LoginLogQueryDto dto)
    {
        var result = await _service.ExportLogin(dto);
        if (result.Successful)
        {
            return ExportExcel(result.Data);
        }

        return Ok(result);
    }
}