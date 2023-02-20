using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf;
using CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MdKaBigSysNameConf", AddToDocument = true, Description = "KA大系统")]
    public class MdKaBigSysNameConfController : Web.ModuleController
    {
        private readonly IMdKaBigSysNameConfService _service;

        public MdKaBigSysNameConfController(IMdKaBigSysNameConfService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MdKaBigSysNameConfQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MdKaBigSysNameConfAddDto dto)
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
        public async Task<IResultModel> Update(MdKaBigSysNameConfUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
