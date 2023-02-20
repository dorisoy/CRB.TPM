using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Employee;
using CRB.TPM.Mod.PS.Core.Application.Employee.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLatestSelect.Dto;
using CRB.TPM.Mod.PS.Core.Application.EmployeeLeaveInfo.Vo;
using CRB.TPM.Mod.PS.Core.Domain.Employee;
using CRB.TPM.Mod.PS.Core.Domain.EmployeeLatestSelect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Web.Controllers;

[SwaggerTag("员工管理")]
public class EmployeeController : ModuleController
{
    private readonly IEmployeeService _service;
    private readonly IEmployeeLatestSelectService  _employeeLatestSelectService;
    private readonly IAccount _loginInfo;

    public EmployeeController(IEmployeeService service, IAccount loginInfo, IEmployeeLatestSelectService employeeLatestSelectService)
    {
        _service = service;
        _employeeLatestSelectService = employeeLatestSelectService;
        _loginInfo = loginInfo;
    }

    #region ==基本信息==


    [HttpGet]
    [Description("查询")]
    [AllowAnonymous]
    public Task<PagingQueryResultModel<EmployeeLatestSelectVo>> Query2([FromQuery] EmployeeLatestSelectQueryDto model)
    {
        return _employeeLatestSelectService.QueryView(Guid.Empty, model);
    }


    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("查询")]
    public Task<PagingQueryResultModel<EmployeeEntity>> Query([FromQuery]EmployeeQueryDto model)
    {
        return _service.Query(model);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("添加")]
    public async Task<IResultModel> Add(EmployeeAddDto dto)
    {
        return await _service.Add(dto);
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
    public Task<IResultModel> Update(EmployeeUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 离职
    /// </summary>
    /// <param name="vo"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("离职")]
    public Task<IResultModel> Leave(EmployeeLeaveVo vo)
    {
        return _service.Leave(vo);
    }

    /// <summary>
    /// 获取离职信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("获取离职信息")]
    public Task<IResultModel> LeaveInfo([BindRequired] Guid id)
    {
        return _service.GetLeaveInfo(id);
    }

    #endregion


    #region ==账户更新==

    /// <summary>
    /// 账户编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("账户编辑")]
    public Task<IResultModel> EditAccount([BindRequired] Guid id)
    {
        return _service.EditAccount(id);
    }

    /// <summary>
    /// 账户更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Description("账户更新")]
    public Task<IResultModel> UpdateAccount(EmployeeUpdateDto dto)
    {
        return _service.UpdateAccount(dto);
    }

    #endregion

    #region ==人员选择==

    /// <summary>
    /// 查询同一部门下的人员信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Description("查询同一部门下的人员信息")]
    public Task<PagingQueryResultModel<EmployeeEntity>> QueryWithSameDepartment([FromQuery]EmployeeQueryDto dto)
    {
        return _service.QueryWithSameDepartment(_loginInfo.Id, dto);
    }

    /// <summary>
    /// 查询最近选择人员列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Description("查询最近选择人员列表")]
    public Task<PagingQueryResultModel<EmployeeLatestSelectEntity>> QueryLatestSelect([FromQuery]EmployeeLatestSelectQueryDto model)
    {
        return _service.QueryLatestSelect(_loginInfo.Id, model);
    }

    /// <summary>
    /// 保存最近人员选择记录
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowWhenAuthenticated]
    [Description("保存最近人员选择记录")]
    public Task<IResultModel> SaveLatestSelect([FromBody]List<Guid> ids)
    {
        return _service.SaveLatestSelect(_loginInfo.Id, ids);
    }

    /// <summary>
    /// 查询人员树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Description("查询人员树")]
    public Task<IResultModel> Tree()
    {
        return _service.GetTree();
    }

    /// <summary>
    /// 批量查询人员基本信息
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    [Description("批量查询人员基本信息")]
    public Task<IResultModel> BaseInfoList([FromQuery]List<Guid> ids)
    {
        return _service.GetBaseInfoList(ids);
    }

    #endregion
}
