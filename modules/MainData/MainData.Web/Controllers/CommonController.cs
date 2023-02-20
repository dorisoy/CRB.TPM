using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using NSwag.Annotations;

namespace CRB.TPM.Mod.MainData.Web.Controllers;

[OpenApiTag("Common", AddToDocument = true, Description = "主数据通用功能")]
[AllowWhenAuthenticated]
public class CommonController : Web.ModuleController
{
    private readonly IPlatformProvider _platformProvider;
    public CommonController( IPlatformProvider platformProvider)
    {
        _platformProvider = platformProvider;
    }

    /// <summary>
    /// 主数据组织架构层级枚举下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("主数据组织架构层级枚举下拉列表")]
    public IResultModel OrgEnumTypeSelect()
    {
        return ResultModel.Success(EnumExtensions.ToResult<OrgEnumType>());
    }
}