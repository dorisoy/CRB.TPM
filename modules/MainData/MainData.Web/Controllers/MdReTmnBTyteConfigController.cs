using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig;
using CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MdReTmnBTyteConfig", AddToDocument = true, Description = "终端业态关系表")]
    public class MdReTmnBTyteConfigController : Web.ModuleController
    {
        private readonly IMdReTmnBTyteConfigService _service;

        public MdReTmnBTyteConfigController(IMdReTmnBTyteConfigService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MdReTmnBTyteConfigQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MdReTmnBTyteConfigAddDto dto)
        {
            return await _service.Add(dto);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MdReTmnBTyteConfigUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
