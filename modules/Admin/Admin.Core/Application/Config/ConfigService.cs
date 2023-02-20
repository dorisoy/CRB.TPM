using CRB.TPM.Config.Abstractions;
using CRB.TPM.Config.Core;
using CRB.TPM.Mod.Admin.Core.Application.Config.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Config.Vo;
using CRB.TPM.Mod.Admin.Core.Domain.Config;
using CRB.TPM.Mod.Admin.Core.Infrastructure;
using System.Security.Permissions;

namespace CRB.TPM.Mod.Admin.Core.Application.Config;

public class ConfigService : IConfigService
{
    private readonly IConfigRepository _repository;
    private readonly IConfigProvider _configProvider;
    public ConfigService(IConfigRepository repository, IConfigProvider configProvider)
    {
        _repository = repository;
        _configProvider = configProvider;
    }

    /// <summary>
    /// 获取前端配置
    /// </summary>
    /// <returns></returns>
    public UIConfigResultVo GetUI()
    {
        var result = new UIConfigResultVo();

        #region ==系统信息==

        var systemConfig = _configProvider.Get<SystemConfig>();
        result.System = new UISystemVo
        {
            Title = systemConfig.Title,
            Logo = systemConfig.Logo,
            Copyright = systemConfig.Copyright
        };

        #endregion


        #region ==权限配置==

        var authConfig = _configProvider.Get<AuthConfig>();
        result.Permission = new UIPermissionVo
        {
            Validate = authConfig.Validate,
            Button = authConfig.Button
        };

        #endregion

        #region ==组件配置==

        result.Component = _configProvider.Get<ComponentConfig>();
        result.Component.Login.VerifyCode = authConfig.VerifyCode;
        #endregion

        return result;
    }

    /// <summary>
    /// 编辑配置
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public IResultModel Edit(string code, ConfigType type)
    {
        var ret = _configProvider.Get(code, type);
        return ResultModel.Success(ret);
    }

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public IResultModel Update(ConfigUpdateDto dto)
    {
        var ret = _configProvider.Set(dto.Type, dto.Code, dto.Json);
        return ResultModel.Success(ret);
    }
}
