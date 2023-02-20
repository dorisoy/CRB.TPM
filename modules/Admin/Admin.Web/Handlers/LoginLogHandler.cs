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
/// 登录日志处理
/// </summary>
[SingletonInject]
public class LoginLogHandler : ILoginLogHandler
{
    public Task Handle(LoginBaseResult loginResult)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Write(LoginLogModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> WriteLog(Exception ex)
    {
        throw new NotImplementedException();
    }
}
