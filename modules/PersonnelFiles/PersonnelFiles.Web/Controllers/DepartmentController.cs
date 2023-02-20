using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Domain.Menu;
using CRB.TPM.Mod.PS.Core.Application.Department;
using CRB.TPM.Mod.PS.Core.Application.Department.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Web.Controllers;

[SwaggerTag("部门管理")]
public class DepartmentController : Web.ModuleController
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// 部门树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("部门树")]
    [AllowAnonymous]
    public Task<IResultModel> Tree()
    {
        return _service.GetTree();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("查询")]
    public Task<PagingQueryResultModel<DepartmentEntity>> Query([FromQuery]DepartmentQueryDto dto)
    {
        return _service.Query(dto);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("添加")]
    public Task<IResultModel> Add(DepartmentAddDto dto)
    {
        return _service.Add(dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Description("删除")]
    public Task<IResultModel> Delete([BindRequired] Guid id)
    {
        return _service.Delete(id);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("编辑")]
    public Task<IResultModel> Edit([BindRequired] Guid id)
    {
        return _service.Edit(id);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("修改")]
    public Task<IResultModel> Update(DepartmentUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 更改排序
    /// </summary>
    /// <param name="departments"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultModel> UpdateSort(IList<DepartmentEntity> departments)
    {
        return _service.UpdateSort(departments);
    }
}
