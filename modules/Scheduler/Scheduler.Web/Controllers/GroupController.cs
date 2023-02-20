using CRB.TPM.Mod.Scheduler.Core.Application.Group;
using CRB.TPM.Mod.Scheduler.Core.Application.Group.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Web.Controllers;

[OpenApiTag("Group", AddToDocument = true, Description = "任务组管理")]
public class GroupController : ModuleController
{
    private readonly IGroupService _service;

    public GroupController(IGroupService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询任务组
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("查询")]
    public async Task<IResultModel> Query([FromQuery]GroupQueryDto dto)
    {
        return await _service.Query(dto);
    }

    /// <summary>
    /// 添加任务组
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("添加")]
    public async Task<IResultModel> Add(GroupAddDto dto)
    {
        return await _service.Add(dto);
    }

    /// <summary>
    /// 删除任务组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Description("删除")]
    public Task<IResultModel> Delete([BindRequired]Guid id)
    {
        return _service.Delete(id);
    }

    /// <summary>
    /// 任务组下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public Task<IResultModel> Select()
    {
        return _service.Select();
    }
}
