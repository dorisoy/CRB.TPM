using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.PS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace CRB.TPM.Mod.PS.Web.Controllers;


[SwaggerTag("公司单位信息")]
public class CompanyController : Web.ModuleController
{
    private readonly IConfigProvider _configProvider;

    public CompanyController(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }

    /// <summary>
    /// 获取公司单位信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Description("获取公司单位信息")]
    [AllowWhenAuthenticated]
    public IResultModel Get()
    {
        var config = _configProvider.Get<PSConfig>();
        var company = new
        {
            Name = config.CompanyName,
            Address = config.CompanyAddress,
            Contact = config.CompanyContact,
            Phone = config.CompanyPhone,
            Fax = config.CompanyFax
        };
        return ResultModel.Success(company);
    }
}
