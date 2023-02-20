using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.MainData.Core.Application.SyncAdvertiser;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("SyncAdvertiser", AddToDocument = true, Description = "同步广告商主数据")]
    public class SyncAdvertiserController : Web.ModuleController
    {
        private readonly ISyncAdvertiserService _service;

        public SyncAdvertiserController(ISyncAdvertiserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("同步广告商主数据")]
        public async Task<IResultModel> Sync(string date, string marketOrgCode)
        {
            return await _service.SyncData(date, marketOrgCode);
        }
    }
}
