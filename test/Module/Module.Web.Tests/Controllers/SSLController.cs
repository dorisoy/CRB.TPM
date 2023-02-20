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
using CRB.TPM.Utils.Enums;
using CRB.TPM.Module.Web.Attributes;

namespace CRB.TPM.Mod.Module.Web.Controllers;

[SwaggerTag("SSL测试")]
[AllowAnonymous]
public class SSLController : ModuleController
{
    public SSLController()
    {
    }

    [HttpGet]
    [HttpsRequirement(SslRequirement.Yes)]
    public string Query()
    {
        return "";
    }
}