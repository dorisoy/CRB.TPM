using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Options;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Logging.Abstractions.Providers;
using CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo;
using CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;
using CRB.TPM.Module.Abstractions;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Web.AuditingHandler;

/// <summary>
/// 审计日志处理
/// </summary>
[SingletonInject]
public class AuditingHandler : IAuditLogHandler
{
    private readonly MvcHelper _mvcHelper;
    private readonly IAccount _account;
    private readonly IAuditInfoService _auditInfoService;
    private readonly IModuleCollection _moduleCollection;
    private readonly ILogger<AuditingHandler> _logger;
    private readonly IConfigProvider _configProvider;
    private readonly IOptionsMonitor<AuthOptions> _authOptions;


    public AuditingHandler(MvcHelper mvcHelper, IAccount account, IAuditInfoService auditInfoService,
        IModuleCollection moduleCollection,
        ILogger<AuditingHandler> logger,
        IConfigProvider configProvider,
        IOptionsMonitor<AuthOptions> authOptions)
    {
        _mvcHelper = mvcHelper;
        _account = account;
        _auditInfoService = auditInfoService;
        _moduleCollection = moduleCollection;
        _logger = logger;
        _configProvider = configProvider;
        _authOptions = authOptions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<bool> Write(AuditLogModel model)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task Hand(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = _authOptions.CurrentValue;
        if (!config.EnableAuditLog)
        {
            await next();
            return;
        }

        var auditInfo = CreateAuditInfo(context);

        var sw = new Stopwatch();
        sw.Start();

        var resultContext = await next();

        sw.Stop();

        if (auditInfo != null)
        {
            try
            {
                //执行结果
                if (resultContext.Result is ObjectResult result)
                {
                    auditInfo.Result = JsonSerializer.Serialize(result.Value);
                }

                //用时
                auditInfo.ExecutionDuration = sw.ElapsedMilliseconds;

                await _auditInfoService.Add(auditInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("审计日志插入异常：{@ex}", ex);
            }
        }
    }

    private AuditInfoEntity CreateAuditInfo(ActionExecutingContext context)
    {
        try
        {
            var routeValues = context.ActionDescriptor.RouteValues;
            var auditInfo = new AuditInfoEntity
            {
                AccountId = _account.Id,
                AccountName = _account.AccountName,
                Area = routeValues["area"] ?? "",
                Controller = routeValues["controller"],
                Action = routeValues["action"],
                Parameters = JsonSerializer.Serialize(context.ActionArguments),
                Platform = _account.Platform,
                IP = _account.IP,
                ExecutionTime = DateTime.Now
            };

            //获取模块的名称
            if (auditInfo.Area.NotNull())
            {
                auditInfo.Module = _moduleCollection.FirstOrDefault(m => m.Code.EqualsIgnoreCase(auditInfo.Area))?.Name;
            }

            var controllerDescriptor = _mvcHelper.GetAllController().FirstOrDefault(m => m.Area.NotNull() && m.Area.EqualsIgnoreCase(auditInfo.Area) && m.Name.EqualsIgnoreCase(auditInfo.Controller));
            if (controllerDescriptor != null)
            {
                auditInfo.ControllerDesc = controllerDescriptor.Description;

                var actionDescription = _mvcHelper.GetAllAction().FirstOrDefault(m => m.Controller == controllerDescriptor && m.Name.EqualsIgnoreCase(auditInfo.Action));
                if (actionDescription != null)
                {
                    auditInfo.ActionDesc = actionDescription.Description;
                }
            }


            //记录浏览器UA
            if (_account.Platform == new PlatformCollection().Web.Value)
            {
                auditInfo.BrowserInfo = context.HttpContext.Request.Headers["User-Agent"];
            }

            return auditInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError("审计日志创建异常：{@ex}", ex);
        }

        return null;
    }
}
