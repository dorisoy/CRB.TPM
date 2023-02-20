using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.Account;
using CRB.TPM.Mod.Admin.Core.Application.Account.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using CRB.TPM.Mod.Admin.Core.Application.SyncAccount;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
using CRB.TPM.Mod.Admin.Core.Application.Account.Vo;
using NSwag.Annotations;
using System.ComponentModel;
using System.Linq;

namespace CRB.TPM.Mod.Admin.Web.Controllers;


[OpenApiTag("Account", AddToDocument = true, Description = "账户管理")]
public class AccountController : Web.ModuleController
{
    private readonly IAccountService _service;
    private readonly IAccount _account;
    private readonly IConfigProvider _configProvider;
    private readonly ISyncAccountService _syncAccountService;

    public AccountController(IAccountService service,
        ISyncAccountService syncAccountService,
        IAccount account, 
        IConfigProvider configProvider)
    {
        _service = service;
        _account = account;
        _configProvider = configProvider;
        _syncAccountService = syncAccountService;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <remarks>查询账户列表</remarks>
    [HttpGet]
    public async Task<PagingQueryResultModel<AccountEntity>> Query([FromServices] IAccountResolver accountResolver, [FromQuery] AccountQueryDto dto)
    {
#if DEBUG
       // var orgs = await accountResolver.CurrentMOrgs().ToListAsync(); 
#endif
        return await _service.Query(dto);
    }

    /// <summary>
    /// 查询账户下拉列表（带分页）
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PagingQueryResultModel<dynamic>> Select([FromQuery] AccountSelectQueryDto dto)
    {
        return await _service.Query(dto);
    }

    ///// <summary>
    ///// 查询账户下拉列表（带分页）
    ///// </summary>
    ///// <param name="dto"></param>
    ///// <returns></returns>
    //[HttpGet]
    //[AllowAnonymous]
    //public async Task<PagingQueryResultModel<AccountSelectVo>> Select2([FromQuery] AccountSelectQueryDto dto)
    //{
    //    return await _service.Query2(dto);
    //}

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Add(AccountAddDto dto)
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
    public Task<IResultModel> Update(AccountUpdateDto dto)
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
    /// 获取账户默认密码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IResultModel DefaultPassword()
    {
        var config = _configProvider.Get<AdminConfig>();
        return ResultModel.Success(config.DefaultPassword);
    }

    /// <summary>
    /// 更新账户皮肤配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowWhenAuthenticated]
    [HttpPost]
    public Task<IResultModel> UpdateSkin(AccountSkinUpdateDto dto)
    {
        dto.AccountId = _account.Id;

        return _service.UpdateSkin(dto);
    }


    /// <summary>
    /// 更新账户角色组织
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResultModel> UpdateAccountRoleOrg(AccountRoleOrgUpdateDto dto)
    {
        var ret = await _service.UpdateAccountRoleOrg(dto);
        return ResultModel.Success(ret);
    }

}