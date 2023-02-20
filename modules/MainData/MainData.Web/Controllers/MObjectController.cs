using CRB.TPM.Mod.Admin.Core.Application.MObject;
using CRB.TPM.Mod.Admin.Core.Application.MObject.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NSwag.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MObject", AddToDocument = true, Description = "对象管理")]
    public class MObjectController : Web.ModuleController
    {
        private readonly IMObjectService _service;

        public MObjectController(IMObjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MObjectQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MObjectAddDto dto)
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
        public async Task<IResultModel> Update(MObjectUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
