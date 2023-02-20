using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CRB.TPM.Logging.Abstractions.Providers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRB.TPM.Logging.Core.DefaultProviders;

internal class AuditLogHandler : IAuditLogHandler
{
    private readonly ILogger<AuditLogHandler> _logger;

    public AuditLogHandler(ILogger<AuditLogHandler> logger)
    {
        _logger = logger;
    }

    public Task Hand(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("Audit Log：{@context}", context);
        return Task.CompletedTask;
    }

    public Task<bool> Write(AuditLogModel model)
    {
        _logger.LogInformation("Audit Log：{@model}", model);
        return Task.FromResult(true);
    }
}
