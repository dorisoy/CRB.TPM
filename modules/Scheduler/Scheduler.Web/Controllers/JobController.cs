using CRB.TPM.Mod.Scheduler.Core.Application.Job;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobHttp.Dto;
using CRB.TPM.Mod.Scheduler.Core.Application.JobLog.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;
using CRB.TPM.TaskScheduler.Abstractions.Quartz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Scheduler.Web.Controllers;

[OpenApiTag("Job", AddToDocument = true, Description = "任务管理")]
public class JobController : ModuleController
{
    private readonly IJobService _service;
    private readonly IQuartzModuleCollection _moduleCollection;

    public JobController(IJobService service, IQuartzModuleCollection moduleCollection)
    {
        _service = service;
        _moduleCollection = moduleCollection;
    }

    /// <summary>
    /// 查询任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("查询任务")]
    public async Task<IResultModel> Query([FromQuery]JobQueryDto dto)
    {
        return await _service.Query(dto);
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("添加")]
    public async Task<IResultModel> Add(JobAddDto dto)
    {
        return await _service.Add(dto);
    }

    /// <summary>
    /// 编辑任务
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
    /// 修改任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("修改")]
    public async Task<IResultModel> Update(JobUpdateDto dto)
    {
        return await _service.Update(dto);
    }

    /// <summary>
    /// 删除任务
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
    /// 暂停任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("暂停")]
    public async Task<IResultModel> Pause([BindRequired]Guid id)
    {
        return await _service.Pause(id);
    }

    /// <summary>
    /// 恢复任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("恢复")]
    public async Task<IResultModel> Resume([BindRequired]Guid id)
    {
        return await _service.Resume(id);
    }

    /// <summary>
    /// 停止任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("停止")]
    public async Task<IResultModel> Stop([BindRequired]Guid id)
    {
        return await _service.Stop(id);
    }

    /// <summary>
    /// 任务日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("日志")]
    public async Task<IResultModel> Log([FromQuery] JobLogQueryDto dto)
    {
        return await _service.Log(dto);
    }

    /// <summary>
    /// 选择任务下拉
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public IResultModel JobSelect(string moduleCode)
    {
        var module = _moduleCollection.FirstOrDefault(m => m.Module.Code == moduleCode);
        if (module == null)
            return ResultModel.Failed();

        return ResultModel.Success(module.TaskSelect);
    }

    /// <summary>
    /// 添加HTTP任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("添加HTTP任务")]
    public async Task<IResultModel> AddHttpJob(JobHttpAddDto dto)
    {
        return await _service.AddHttpJob(dto);
    }

    /// <summary>
    /// 编辑HTTP任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("编辑HTTP任务")]
    public async Task<IResultModel> EditHttpJob([BindRequired]Guid id)
    {
        return await _service.EditHttpJob(id);
    }

    /// <summary>
    /// 修改HTTP任务
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("修改HTTP任务")]
    public async Task<IResultModel> UpdateHttpJob(JobHttpUpdateDto dto)
    {
        return await _service.UpdateHttpJob(dto);
    }

    /// <summary>
    /// 认证方式选择
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public IResultModel AuthTypeSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<AuthType>());
    }

    /// <summary>
    /// 日志内容类型选择
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public IResultModel ContentTypeSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<ContentType>());
    }

    /// <summary>
    ///  HTTP任务详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("HTTP任务详情")]
    public async Task<IResultModel> JobHttpDetails(Guid id)
    {
        return await _service.JobHttpDetails(id);
    }
}
