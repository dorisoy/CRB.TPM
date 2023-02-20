using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Position;
using CRB.TPM.Mod.PS.Core.Application.Position.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Position;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Web.Controllers;

[SwaggerTag("职位管理")]
public class PositionController : Web.ModuleController
{
    private readonly IPositionService _service;

    public PositionController(IPositionService service)
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
    public Task<PagingQueryResultModel<PositionEntity>> Query([FromQuery]PositionQueryDto dto)
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
    public Task<IResultModel> Add(PositionAddDto dto)
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
    public Task<IResultModel> Update(PositionUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 获取职位
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("修改")]
    public Task<IResultModel> Get([BindRequired] Guid id)
    {
        return _service.Get(id);
    }
}
