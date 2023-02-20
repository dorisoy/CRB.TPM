using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CRB.TPM.Mod.Module.Core.Application.Account;
using CRB.TPM.Mod.Module.Core.Application.Account.Dto;
using CRB.TPM.Mod.Module.Core.Domain.Account;
using System.Collections.Generic;

namespace CRB.TPM.Mod.Module.Web.Controllers;

[SwaggerTag("账户测试")]
[AllowAnonymous]
public class AccountController : ModuleController
{
    private readonly IAccountService _service;
    private readonly IAccountClientService _accountClientService;
    private readonly IAccount _account;
    private readonly IConfigProvider _configProvider;

    public AccountController(IAccountService service,
        IAccountClientService accountClientService,
        IAccount account, 
        IConfigProvider configProvider)
    {
        _service = service;
        _accountClientService = accountClientService;
        _account = account;
        _configProvider = configProvider;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <remarks>查询角色列表</remarks>
    [HttpGet]
    public Task<IList<AccountEntity>> Query()
    {
        var rs = _accountClientService.Query();
        var rs2 = _service.Query();
        return rs;
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public async Task<IResultModel> Add()
    {
        for (int i = 1; i <= 1000; i++)
        {
            var dto = new AccountAddDto()
            {
                Username = $"Dorisoy{i}",
                Email = "test@dorisoy.com",
                Name = "Dorisoy{i}",
                Password = "123456",
                Phone = "13002929017",
                RoleId = 0
            };

            await _service.Add(dto);
        }

        return ResultModel.Success();
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
    public Task<IResultModel> Update()
    {
        var dto = new AccountUpdateDto()
        {
            Username = "Dorisoy",
            Email = "test@dorisoy.com",
            Name = "Dorisoy",
            Password = "123456",
            Phone = "13002929018",
            RoleId = 2
        };

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

}