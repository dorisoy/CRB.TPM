using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Logging.Abstractions.Providers;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using CRB.TPM.Utils.Annotations;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure.LoginHandler;

/// <summary>
/// 自定义默认登录日志处理器
/// </summary>
[SingletonInject]
internal class DefaultLoginLogHandler : ILoginLogHandler
{
    private readonly ILoginLogRepository _repository;
    private readonly IAccount _account;

    public DefaultLoginLogHandler(ILoginLogRepository repository, IAccount account)
    {
        _repository = repository;
        _account = account;
    }

    public Task Handle(LoginBaseResult model)
    {

        var entity = new LoginLogEntity
        {
            AccountId = model.AccountId,
            UserName = model.UserName,
            Email = model.Email,
            Error = model.Error,
            LoginMode = model.LoginMode,
            LoginTime = DateTime.Now,
            Phone = model.Phone,
            Platform = model.Platform,
            Success = model.Success,
            IP = _account.IP,
            UserAgent = _account.UserAgent
        };
        return _repository.Add(entity);

    }

    public Task<bool> Write(LoginLogModel model)
    {
        return Task.FromResult(true);
    }

    public Task<bool> WriteLog(Exception ex)
    {
        return Task.FromResult(true);
    }
}
