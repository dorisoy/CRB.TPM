using Microsoft.Extensions.Logging;
using CRB.TPM.Data.Abstractions.Logger;
using System;

namespace CRB.TPM.Data.Core;

/// <summary>
/// 默认日志记录器
/// </summary>
internal class DTPgerProvider : IDTPgerProvider
{
    private readonly ILogger _logger;

    public DTPgerProvider(ILogger<DTPgerProvider> logger)
    {
        _logger = logger;
    }

    public void Write(string action, string sql)
    {
        _logger.LogInformation($"{action}:{sql}");
    }

    public void Write(string action, Exception ex)
    {
        _logger.LogError($"{action}:{ex.Message}:{ex.StackTrace}");
    }
}