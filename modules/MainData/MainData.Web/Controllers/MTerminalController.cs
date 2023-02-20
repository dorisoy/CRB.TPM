using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal;
using CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.MDistributorRelation.Dto;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.MainData.Core.Application.MDistributor.Dto;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MTerminal", AddToDocument = true, Description = "终端信息")]
    public class MTerminalController : Web.ModuleController
    {
        private readonly IMTerminalService _service;

        public MTerminalController(IMTerminalService service)
        {
            _service = service;
        }
        /// <summary>
        /// 终端信息列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MTerminalQueryDto dto)
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
        public async Task<IActionResult> Export([FromQuery]MTerminalQueryExportDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }
        /// <summary>
        /// 新增终端信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MTerminalAddDto dto)
        {
            return await _service.Add(dto);
        }
        /// <summary>
        /// 删除终端信息
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
        /// 编辑终端信息
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
        /// 更新终端信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MTerminalUpdateDto dto)
        {
            return await _service.Update(dto);
        }
        /// <summary>
        /// 终端Select下拉接口
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("终端Select下拉接口")]
        public async Task<IResultModel> Select([FromQuery]MTerminalSelectDto dto)
        {
            return await _service.Select(dto);
        }
    }
}
