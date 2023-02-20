using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty;
using CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MProductMarketingProperty", AddToDocument = true, Description = "营销产品属性")]
    public class MProductMarketingPropertyController : Web.ModuleController
    {
        private readonly IMProductMarketingPropertyService _service;

        public MProductMarketingPropertyController(IMProductMarketingPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MProductMarketingPropertyQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MProductMarketingPropertyAddDto dto)
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
        public async Task<IResultModel> Update(MProductMarketingPropertyUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
