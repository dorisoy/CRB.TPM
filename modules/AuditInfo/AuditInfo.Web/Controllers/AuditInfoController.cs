using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo;
using CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo.Dto;
using CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using CRB.TPM.Auth.Abstractions;
using System.Linq;
using NSwag.Annotations;

namespace CRB.TPM.Mod.AuditInfo.Web.Controllers;

[OpenApiTag("AuditInfo", AddToDocument = true, Description = "审计信息")]
public class AuditInfoController : Web.ModuleController
{
    private readonly IAuditInfoService _service;
    private readonly IPlatformProvider _platformProvider;
    public AuditInfoController(IAuditInfoService service, IPlatformProvider platformProvider)
    {
        _service = service;
        _platformProvider = platformProvider;
    }

    /// <summary>
    /// 审计查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [DisableAudit]
    [Description("审计查询")]
    public async Task<PagingQueryResultModel<AuditInfoEntity>> Query([FromQuery]AuditInfoQueryDto dto)
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
    /// 审计详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [DisableAudit]
    [Description("审计详情")]
    public async Task<IResultModel> Details([BindRequired]int id)
    {
        return  await _service.Details(id);
    }

    /// <summary>
    /// 查询最近一周访问量
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [DisableAudit]
    [AllowWhenAuthenticated]
    [Description("查询最近一周访问量")]
    public async Task<IResultModel> QueryLatestWeekPv()
    {
        return await _service.QueryLatestWeekPv();
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("导出")]
    public async Task<IActionResult> Export(AuditInfoQueryDto dto)
    {
        var result = await _service.Export(dto);
        if (result.Successful)
        {
            return ExportExcel(result.Data);
        }

        return Ok(result);
    }
}
