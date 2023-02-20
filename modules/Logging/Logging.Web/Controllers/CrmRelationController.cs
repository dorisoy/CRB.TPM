using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation;
using CRB.TPM.Mod.Logging.Core.Application.CrmRelation.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRelation;

using Swashbuckle.AspNetCore.Annotations;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Logging.Web.Controllers
{
    [OpenApiTag("CrmRelation", AddToDocument = true, Description = "CRM终端关系")]
    public class CrmRelationController : Web.ModuleController
    {
        private readonly ICrmRelationService _service;

        public CrmRelationController(ICrmRelationService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]CrmRelationQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(CrmRelationAddDto dto)
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
        public async Task<IResultModel> Update(CrmRelationUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
