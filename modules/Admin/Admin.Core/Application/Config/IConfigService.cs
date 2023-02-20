using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.Admin.Core.Application.Config.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Config.Vo;

namespace CRB.TPM.Mod.Admin.Core.Application.Config
{
    public interface IConfigService
    {
        IResultModel Edit(string code, ConfigType type);
        UIConfigResultVo GetUI();
        IResultModel Update(ConfigUpdateDto dto);
    }
}