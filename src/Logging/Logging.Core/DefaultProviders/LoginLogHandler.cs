using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CRB.TPM.Logging.Abstractions.Providers;
using CRB.TPM.Auth.Abstractions;
using System;
namespace CRB.TPM.Logging.Core.DefaultProviders;

/// <summary>
/// 默认登录日志处理器
/// </summary>
internal class LoginLogHandler : ILoginLogHandler
{
    private readonly ILogger<LoginLogHandler> _logger;

    public LoginLogHandler(ILogger<LoginLogHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LoginBaseResult loginResult)
    {
        _logger.LogInformation("login Result：{@loginResult}", loginResult);
        return Task.FromResult(true);
    }

    public Task<bool> Write(LoginLogModel model)
    {
        _logger.LogInformation("Login Log：{@model}", model);

        return Task.FromResult(true);
    }

    public Task<bool> WriteLog(Exception ex)
    {
        _logger.LogInformation("Exception：{@ex}", ex);

        return Task.FromResult(true);
    }
}