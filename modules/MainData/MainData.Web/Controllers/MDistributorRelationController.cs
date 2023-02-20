using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MDistributorRelation;

using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;
using System.Collections.Generic;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MDistributorRelation", AddToDocument = true, Description = "经销分销关系")]
    public class MDistributorRelationController : Web.ModuleController
    {
        private readonly IMDistributorRelationService _service;

        public MDistributorRelationController(IMDistributorRelationService service)
        {
            _service = service;
        }
        /// <summary>
        /// 经销分销关系列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MDistributorRelationQueryDto dto)
        {
            return await _service.Query(dto);
        }
        /// <summary>
        /// 导出经销分销关系列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAudit]
        [Description("导出")]
        public async Task<IActionResult> Export([FromQuery]MDistributorRelationQueryDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }
        /// <summary>
        /// 新增经销分销关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MDistributorRelationAddDto dto)
        {
            return await _service.Add(dto);
        }
        /// <summary>
        /// 删除经销分销关系
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
        /// 批量删除客户信息
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
        /// 编辑经销分销关系
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
        /// 更新编辑经销分销关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MDistributorRelationUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
