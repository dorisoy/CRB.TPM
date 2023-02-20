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
using CRB.TPM.Mod.MainData.Core.Application.SyncSetting;
using Microsoft.AspNetCore.Authorization;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("SyncSetting", AddToDocument = true, Description = "同步配置信息测试")]
    public class SyncSettingController : Web.ModuleController
    {
        private readonly ISyncSettingService _service;

        public SyncSettingController(ISyncSettingService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("同步配置信息测试")]
        public async Task<IResultModel> Sync(string date)
        {
            return await _service.SyncData(date);
        }
    }
}
