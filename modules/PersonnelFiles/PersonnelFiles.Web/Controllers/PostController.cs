using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Post;
using CRB.TPM.Mod.PS.Core.Application.Post.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Web.Controllers;


[SwaggerTag("岗位管理")]
public class PostController : Web.ModuleController
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("查询")]
    public Task<PagingQueryResultModel<PostEntity>> Query([FromQuery]PostQueryDto dto)
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
    public Task<IResultModel> Add(PostAddDto dto)
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
    public Task<IResultModel> Update(PostUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Description("下拉列表")]
    public Task<IResultModel> Select()
    {
        return _service.Select();
    }
}
