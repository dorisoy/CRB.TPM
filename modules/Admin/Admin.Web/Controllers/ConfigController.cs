using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.Config;
using CRB.TPM.Module.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using CRB.TPM.Utils.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using CRB.TPM.Mod.Admin.Core.Application.Config.Dto;
using Newtonsoft.Json;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("Config", AddToDocument = true, Description = "配置管理")]
[AllowWhenAuthenticated]
public class ConfigController : Web.ModuleController
{
    private readonly IConfigService _service;
    private readonly IConfigCollection _configCollection;
    private readonly IFileStorageProvider _fileStorageProvider;

    public ConfigController(IConfigService service, IConfigCollection configCollection, IFileStorageProvider fileStorageProvider)
    {
        _service = service;
        _configCollection = configCollection;
        _fileStorageProvider = fileStorageProvider;
    }

    [HttpGet]
    [AllowAnonymous]
    [Description("UI配置信息")]
    [DisableAudit]
    public IResultModel UI()
    {
        var result = _service.GetUI();
        result.System.Logo = _fileStorageProvider.GetUrl(result.System.Logo);
        return ResultModel.Success(result);
    }

    [HttpGet]
    [Description("编辑")]
    [AllowAnonymous]
    public IResultModel Edit(string code, ConfigType type)
    {

        var ret = _service.Edit(code, type);
        var json = JsonConvert.SerializeObject(ret);
        return ret;
    }

    [HttpPost]
    [Description("保存")]
    public IResultModel Update(ConfigUpdateDto dto)
    {
        return _service.Update(dto);
    }

    [HttpGet]
    [Description("配置描述符列表")]
    public IResultModel Descriptors()
    {
        var list = _configCollection;
        return ResultModel.Success(list);
    }
}

