using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.Module.Core.Application.Account;
using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using System.Collections.Generic;
using System.ComponentModel;
using CRB.TPM.Excel.Abstractions.Export;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace CRB.TPM.Mod.Module.Web.Controllers;

[SwaggerTag("账户测试")]
[AllowAnonymous]
public class ExportController : ModuleController
{
    private readonly IAccountService _service;
    private readonly IAccountClientService _accountClientService;
    private readonly IAccount _account;
    private readonly IConfigProvider _configProvider;

    public ExportController(IAccountService service,
        IAccountClientService accountClientService,
        IAccount account,
        IConfigProvider configProvider)
    {
        _service = service;
        _accountClientService = accountClientService;
        _account = account;
        _configProvider = configProvider;
    }

    /// <summary>
    /// 导出测试
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Description("导出测试")]
    public async Task<IActionResult> Export(int exportCount = 1000)
    {
        var dto = new AccountQueryDto()
        {
            ExportModel = new ExcelExportEntityModel<AccountEntity>()
            {
                Title = DateTime.Now.ToString("yyyyMMdd"),
                Columns = new List<ExcelExportColumnModel>()
                {
                    new ExcelExportColumnModel
                    {
                        Prop = "Id",
                        Label = "Id",
                        Align= ColumnAlign.Center
                    },
                    new ExcelExportColumnModel
                    {
                        Prop = "Username",
                        Label = "Username",
                        Align= ColumnAlign.Center
                    }
                }
            },
            ExportCount = exportCount
        };

        var result = await _service.Export(dto);
        if (result.Successful)
        {
            return ExportExcel(result.Data);
        }

        return Ok(result);
    }

}