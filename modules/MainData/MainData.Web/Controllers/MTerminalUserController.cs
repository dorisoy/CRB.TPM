using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalUser.Dto;
using CRB.TPM.Mod.MainData.Core.Domain.MTerminalUser;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Application.MTerminalDistributor.Dto;
using System.Collections.Generic;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MTerminalUser", AddToDocument = true, Description = "终端业务员")]
    public class MTerminalUserController : Web.ModuleController
    {
        private readonly IMTerminalUserService _service;

        public MTerminalUserController(IMTerminalUserService service)
        {
            _service = service;
        }
        /// <summary>
        /// 终端业务员列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MTerminalUserQueryDto dto)
        {
            return await _service.Query(dto);
        }
        /// <summary>
        /// 导出业务员列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAudit]
        [Description("导出")]
        public async Task<IActionResult> Export([FromQuery] MTerminalUserQueryDto dto)
        {
            var result = await _service.Export(dto);
            if (result.Successful)
            {
                return ExportExcel(result.Data);
            }

            return Ok(result);
        }
        /// <summary>
        /// 新增终端业务员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MTerminalUserAddDto dto)
        {
            return await _service.Add(dto);
        }
        /// <summary>
        /// 删除终端业务员
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
        /// 批量删除终端业务员
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
        /// 编辑终端业务员
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
        /// 更新终端业务员
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MTerminalUserUpdateDto dto)
        {
            return await _service.Update(dto);
        }
    }
}
