using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    /// <summary>
    /// 营销产品分配
    /// </summary>
    [AllowAnonymous]
    [OpenApiTag("MMarketingProduct", AddToDocument = true, Description = "营销产品分配")]
    public class MMarketingProductController : Web.ModuleController
    {
        private readonly IMMarketingProductService _service;

        public MMarketingProductController(IMMarketingProductService service)
        {
            _service = service;
        }
        /// <summary>
        /// 营销产品分配列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MMarketingProductQueryDto dto)
        {
            return await _service.Query(dto);
        }
        /// <summary>
        /// 导出营销产品分配
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAudit]
        [Description("导出")]
        public async Task<IActionResult> Export([FromQuery] MMarketingProductQueryExportDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }
        /// <summary>
        /// 添加营销产品分配
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MMarketingProductAddDto dto)
        {
            return await _service.Add(dto);
        }
        /// <summary>
        /// 删除营销产品分配
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
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
        /// 编辑营销产品分配
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return await _service.Edit(id);
        }
        /// <summary>
        /// 修改营销产品分配
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MMarketingProductUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
