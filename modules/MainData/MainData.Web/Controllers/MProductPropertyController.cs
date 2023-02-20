using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Vo;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MProductProperty", AddToDocument = true, Description = "产品属性")]
    public class MProductPropertyController : Web.ModuleController
    {
        private readonly IMProductPropertyService _service;

        public MProductPropertyController(IMProductPropertyService service)
        {
            _service = service;
        }
        /// <summary>
        /// 产品属性列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery] MProductPropertyQueryDto dto)
        {
            return await _service.Query(dto);
        }
        /// <summary>
        /// 产品属性列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("产品属性列表")]
        public IResultModel TypeSelect()
        {
            return _service.TypeSelect();
        }
        /// <summary>
        /// 导出产品属性
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAudit]
        [Description("导出")]
        public async Task<IActionResult> Export([FromQuery] MProductPropertyQueryExportDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }
        /// <summary>
        /// 新增产品属性
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MProductPropertyAddDto dto)
        {
            return await _service.Add(dto);
        }
        /// <summary>
        /// 删除产品属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired] Guid id)
        {
            return await _service.Delete(id);
        }
        /// <summary>
        /// 批量删除产品属性
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("批量删除")]
        public async Task<IResultModel> DeleteSelected([FromQuery] IEnumerable<Guid> ids)
        {
            return await _service.DeleteSelected(ids);
        }
        /// <summary>
        /// 编辑产品属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired] Guid id)
        {
            return await _service.Edit(id);
        }
        /// <summary>
        /// 修改产品属性
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MProductPropertyUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
