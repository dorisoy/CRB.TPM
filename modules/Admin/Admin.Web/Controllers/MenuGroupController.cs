using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MenuGroup;
using CRB.TPM.Mod.Admin.Core.Application.MenuGroup.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.MenuGroup;
using Swashbuckle.AspNetCore.Annotations;
using System;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("MenuGroup", AddToDocument = true, Description = "菜单分组")]
public class MenuGroupController : Web.ModuleController
{
    private readonly IMenuGroupService _service;

    public MenuGroupController(IMenuGroupService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    [HttpGet]
    public Task<PagingQueryResultModel<MenuGroupEntity>> Query([FromQuery] MenuGroupQueryDto dto)
    {
        return _service.Query(dto);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Add(MenuGroupAddDto dto)
    {
        return _service.Add(dto);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultModel> Edit(Guid id)
    {
        return _service.Edit(id);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Update(MenuGroupUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public Task<IResultModel> Delete([BindRequired] Guid id)
    {
        return _service.Delete(id);
    }

    /// <summary>
    /// 下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    public Task<IResultModel> Select()
    {
        return _service.Select();
    }
}