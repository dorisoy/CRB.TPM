using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Module.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("Common", AddToDocument = true, Description = "通用功能")]
[AllowWhenAuthenticated]
public class CommonController : Web.ModuleController
{
    private readonly IModuleCollection _moduleCollection;
    private readonly IPlatformProvider _platformProvider;

    public CommonController(IModuleCollection moduleCollection, IPlatformProvider platformProvider)
    {
        _moduleCollection = moduleCollection;
        _platformProvider = platformProvider;
    }

    /// <summary>
    /// 获取枚举中选项列表
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <param name="enumName"></param>
    /// <returns></returns>
    [HttpGet]
    [Description("获取枚举中选项列表")]
    public IResultModel EnumOptions(string moduleCode, string enumName)
    {
        var module = _moduleCollection.FirstOrDefault(m => m.Code.EqualsIgnoreCase(moduleCode));
        if (module == null)
            return ResultModel.Success(new List<OptionResultModel>());

        var enumDescriptor = module.EnumDescriptors.FirstOrDefault(m => m.Name.EqualsIgnoreCase(enumName));
        if (enumDescriptor == null)
            return ResultModel.Success(new List<OptionResultModel>());

        return ResultModel.Success(enumDescriptor.Options);
    }

    /// <summary>
    /// 平台下拉选项
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("平台类型下拉列表")]
    public IResultModel PlatformOptions()
    {
        return ResultModel.Success(_platformProvider.SelectOptions());
    }

    /// <summary>
    /// 登录模式下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("登录模式下拉列表")]
    public IResultModel LoginModeSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<LoginMode>());
    }

    /// <summary>
    /// 账户类型下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("账户类型下拉列表")]
    public IResultModel AccountTypeSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<AccountType>());
    }

    /// <summary>
    /// 账户状态下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("账户状态下拉列表")]
    public IResultModel AccountStatusSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<AccountStatus>());
    }

    [HttpGet]
    [Description("账户状态下拉列表")]
    public OptionCollectionResultModel API_AccountStatusSelect()
    {
        return EnumExtensions.ToResult<AccountStatus>();
    }
}
