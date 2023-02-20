using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.Module.Core.Application.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Module.Web.Controllers;


[SwaggerTag("测试数据保护")]
[AllowAnonymous]
public class DataProtectionController : ModuleController
{
    private readonly IDataProtectionProvider  _dataProtectionProvider;
    private readonly IConfigProvider _configProvider;

    private readonly IDataProtector _dataProtector;
    private readonly ITimeLimitedDataProtector _timeLimitedDataProtector;

    public DataProtectionController(IDataProtectionProvider dataProtectionProvider, IConfigProvider configProvider)
    {
        _dataProtectionProvider = dataProtectionProvider;
        _configProvider = configProvider;

        //创建数据保护器
        _dataProtector = _dataProtectionProvider.CreateProtector("TPM");
        _timeLimitedDataProtector = _dataProtectionProvider.CreateProtector("TPM_TIMELIMITED").ToTimeLimitedDataProtector();
    }

    /// <summary>
    /// 测试数据保护
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IResultModel CreateProtector()
    {
        //加密
        string protectText = _dataProtector.Protect("password=123456;User id=sa");
        //解密
        var text = _dataProtector.Unprotect(protectText);

        // "protectText": "CfDJ8CMVv474cuVEnersYQH0NLnRBH9YzXvNdCPJ0q5ttST9nt0u3h9vHwlotCkTy9RuGAUjEyza591Km-ooDOKtPY3kBk-SBsDpvdnUzpiCSPRHsnveIMw30E-6mhkVncJEzk1YqyIFkZ-f07U2jQC6uf8"

        //"text": "password=123456;User id=sa"

        return ResultModel.Success(new { protectText, text });
    }


    /// <summary>
    /// 测试限时的数据保护
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IResultModel CreateTimeLimitedProtector()
    {
        //加密（5秒后过期）
        var _protectText = _timeLimitedDataProtector.Protect("password=123456;User id=sa", TimeSpan.FromSeconds(5));
        return ResultModel.Success(new { _protectText });
    }


    /// <summary>
    /// 测试限时的数据保护
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IResultModel GetTimeLimitedProtector(string protectText)
    {
        //var timeLimitedDataProtector =  HttpContext.RequestServices.GetDataProtector("TPM_TIMELIMITED"); 

        var text = "";
        try
        {
            if (!string.IsNullOrEmpty(protectText))
                //解密(5秒后不能解密)
                text = _timeLimitedDataProtector.Unprotect(protectText);
        }
        catch (System.Security.Cryptography.CryptographicException ex)
        {
            text = ex.Message;
        }

        return ResultModel.Success(new { text });
    }

}
