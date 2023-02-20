using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MEntity;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn;
using CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("SyncDtAndTmn", AddToDocument = true, Description = "同步主数据")]
    public class SyncDtAndTmnController : Web.ModuleController
    {
        private readonly ISyncDtAndTmnService _syncDtAndTmnService;

        public SyncDtAndTmnController(ISyncDtAndTmnService syncDtAndTmnService)
        {
            _syncDtAndTmnService = syncDtAndTmnService;
        }

        [HttpGet]
        [Description("同步经销商、终端主数据")]
        public async Task<IResultModel> SyncData(string marketOrgCD)
        {
            SyncDtAndTmnDto dto = new SyncDtAndTmnDto {  MarketOrgCD = marketOrgCD };
            return await _syncDtAndTmnService.SyncData(dto);
        }
    }
}
