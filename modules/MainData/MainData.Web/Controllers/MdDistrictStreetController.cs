using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet;
using CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MdDistrictStreet", AddToDocument = true, Description = "区县街道")]
    public class MdDistrictStreetController : Web.ModuleController
    {
        private readonly IMdDistrictStreetService _service;

        public MdDistrictStreetController(IMdDistrictStreetService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MdDistrictStreetQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MdDistrictStreetAddDto dto)
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
        public async Task<IResultModel> Update(MdDistrictStreetUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
