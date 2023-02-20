using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress;
using CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;

using Swashbuckle.AspNetCore.Annotations;
using NSwag.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [OpenApiTag("MAdvertiserAccountAddress", AddToDocument = true, Description = "广告商地点分配表")]
    public class MAdvertiserAccountAddressController : Web.ModuleController
    {
        private readonly IMAdvertiserAccountAddressService _service;

        public MAdvertiserAccountAddressController(IMAdvertiserAccountAddressService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MAdvertiserAccountAddressQueryDto dto)
        {
            return await _service.Query(dto);
        }

        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MAdvertiserAccountAddressAddDto dto)
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
        public async Task<IResultModel> Update(MAdvertiserAccountAddressUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
