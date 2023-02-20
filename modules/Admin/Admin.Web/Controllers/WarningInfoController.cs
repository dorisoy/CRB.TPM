using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo;
using CRB.TPM.Mod.Admin.Core.Application.WarningInfo.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("WarningInfo", AddToDocument = true, Description = "预警信息")]
public class WarningInfoController : Web.ModuleController
{
    private readonly IWarningInfoService _service;

    public WarningInfoController(IWarningInfoService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    [HttpGet]
    public async Task<PagingQueryResultModel<WarningInfoEntity>> Query(WarningInfoQueryDto dto)
    {
        return await _service.Query(dto);
    }

}
