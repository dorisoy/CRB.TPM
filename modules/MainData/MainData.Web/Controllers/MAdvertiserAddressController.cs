using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MAdvertiserAddress", AddToDocument = true, Description = "广告商地点表")]
    public class MAdvertiserAddressController : Web.ModuleController
    {
        private readonly IMAdvertiserAddressService _service;

        public MAdvertiserAddressController(IMAdvertiserAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MAdvertiserAddressQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MAdvertiserAddressAddDto dto)
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
        public async Task<IResultModel> Update(MAdvertiserAddressUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
