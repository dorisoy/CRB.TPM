using CRB.TPM.Mod.Admin.Core.Application.MOrg;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Web.Controllers
{
    [OpenApiTag("MOrg", AddToDocument = true, Description = "组织管理")]
    public class MOrgController : Web.ModuleController
    {
        private readonly IMOrgService _service;

        public MOrgController(IMOrgService service)
        {
            _service = service;
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
        public async Task<IResultModel> CurrentMOrg()
        {
            var ret = await _service.CurrentMOrg();
            return ResultModel.Success(ret);
        }


        /// <summary>
        /// 获取指定层级组织数据
        /// </summary>
        /// <param name="level">10:雪花、20:事业部、30:营销中心、40:大区、50:业务部、60:工作站、70:客户</param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取指定层级组织数据")]
        public async Task<IResultModel> GetOrgLevel(int? level = 10, bool ignore = false)
        {
            return await _service.GetOrgLevel(level, ignore);
        }

        /// <summary>
        /// 获取指定层级组织
        /// </summary>
        /// <param name="level">表示指定获取几个层级的数据,默认获取1层,10:雪花、20:事业部、
        /// 30:营销中心、40:大区、50:业务部、60:工作站、70:客户</param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取组织树")]
        public async Task<IResultModel> Tree(int? level = 10, bool metadata = false)
        {
            return await _service.GetTree(level, metadata);
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
        public async Task<IResultModel> GetTreeByParentId(int? level = 10, Guid? parentId = null)
        {
            return await _service.GetTreeByParentId(level, parentId);
        }

        /// <summary>
        /// 根据父ID获取组织节点
        /// </summary>
        /// <param name="level"></param>
        /// <param name="parentId"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("根据父ID获取组织节点")]
        public async Task<IResultModel> GetNodeByParentId(int? level = 10, Guid? parentId = null, bool ignore = false)
        {
            return await _service.GetNodeByParentId(level, parentId, ignore);
        }
    }
}
