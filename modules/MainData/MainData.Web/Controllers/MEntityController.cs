using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MEntity;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn;
using CRB.TPM.Excel.Abstractions;
using System.Collections.Generic;
using CRB.TPM.Auth.Abstractions.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MEntity", AddToDocument = true, Description = "法人主体")]
    public class MEntityController : Web.ModuleController
    {
        private readonly IMEntityService _service;
        private readonly ISyncDtAndTmnService _syncDtAndTmnService;

        public MEntityController(IMEntityService service, ISyncDtAndTmnService syncDtAndTmnService)
        {
            _service = service;
            _syncDtAndTmnService = syncDtAndTmnService;
        }

        /// <summary>
        /// 法人主体列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MEntityQueryDto dto)
        {
            return await _service.Query(dto);
        }

        /// <summary>
        /// 导出法人主体列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAudit]
        [Description("导出")]
        public async Task<IActionResult> Export([FromQuery] MEntityQueryDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }

        /// <summary>
        /// 新增法人主体
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MEntityAddDto dto)
        {
            return await _service.Add(dto);
        }

        /// <summary>
        /// 删除法人主体
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
        /// 批量删除法人主体
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
        /// 编辑法人主体
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
        /// 更新法人主体
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MEntityUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
