using CRB.TPM.Mod.Admin.Core.Application.MOrg;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NSwag.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers
{
    [AllowAnonymous]
    [OpenApiTag("MOrg", AddToDocument = true, Description = "组织树")]
    public class MOrgController : Web.ModuleController
    {
        private readonly IMOrgService _service;

        public MOrgController(IMOrgService service)
        {
            _service = service;
        }

        /// <summary>
        /// 查询组织
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]MOrgQueryDto dto)
        {
            return await _service.Query(dto);
        }

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IResultModel> Add(MOrgAddDto dto)
        {
            return await _service.Add(dto);
        }

        /// <summary>
        /// 删除组织
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
        /// 批量删除组织
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
        /// 编辑组织
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
        /// 更新组织
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改")]
        public async Task<IResultModel> Update(MOrgUpdateDto dto)
        {
            return await _service.Update(dto);
        }

        /// <summary>
        /// 组织树结构列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("组织树结构列表")]
        public async Task<IResultModel> Select([FromQuery]OrgSelectDto dto)
        {
            return await _service.Select(dto);
        }


        /// <summary>
        /// 获取当前访问用户的角色拥有的组织层级（合并用户角色和组织）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<IResultModel> CurrentMOrg()
        {
            var ret = await _service.CurrentMOrg();
            return ResultModel.Success(ret);
        }


        /// <summary>
        /// 获取指定层级组织数据
        /// </summary>
        /// <param name="level">10:雪花、20:事业部、30:营销中心、40:大区、50:业务部、60:工作站、70:客户</param>
        /// <param name="ignore">是否忽略组织</param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取指定层级组织数据")]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<IResultModel> GetOrgLevel(int? level = 10, bool ignore = false)
        {
            return await _service.GetOrgLevel(level);
        }

        /// <summary>
        /// 获取指定层级组织
        /// </summary>
        /// <param name="level">表示指定获取几个层级的数据,默认获取1层,10:雪花、20:事业部、
        /// 30:营销中心、40:大区、50:业务部、60:工作站、70:客户</param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取组织树")]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<IResultModel> Tree(int? level = 10)
        {
            var tree = await _service.GetTree(level);
            return tree;
        }

        /// <summary>
        /// 获取组织树
        /// </summary>
        /// <param name="level">表示指定获取几个层级的数据,默认1层,10:雪花、20:事业部、
        /// 30:营销中心、40:大区、50:业务部、60:工作站、70:客户</param>
        /// <param name="parentId">组织父级ID</param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取组织树")]
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<IResultModel> GetTreeByParentId(int? level = 10, Guid? parentId = null)
        {
            var tree = await _service.GetTreeByParentId(level, parentId);
            return tree;
        }

    }
}
