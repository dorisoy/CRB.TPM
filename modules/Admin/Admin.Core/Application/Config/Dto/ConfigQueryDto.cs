using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Application.Config.Dto;
using CRB.TPM.Mod.Admin.Core.Application.Config.Vo;

namespace CRB.TPM.Mod.Admin.Core.Application.Config.Dto;

public class ConfigQueryDto : QueryDto
{
    public string Key { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public ConfigType? Type { get; set; }
}
