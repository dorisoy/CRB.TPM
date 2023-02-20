using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Application.CrmData;
using CRB.TPM.Mod.Logging.Core.Application.CrmData.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmData;

using Swashbuckle.AspNetCore.Annotations;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Logging.Web.Controllers
{
    [OpenApiTag("CrmData", AddToDocument = true, Description = "CRM终端数据")]
    public class CrmDataController : Web.ModuleController
    {
        private readonly ICrmDataService _service;

        public CrmDataController(ICrmDataService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]CrmDataQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(CrmDataAddDto dto)
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
        public async Task<IResultModel> Update(CrmDataUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
