using CRB.TPM.Config.Abstractions;
using CRB.TPM.Data.Abstractions.Query;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Admin.Core.Application.Config.Dto;

public class ConfigUpdateDto
{
    [Required(ErrorMessage = "配置类编码不能为空")]
    public string Code { get; set; }

    public ConfigType Type { get; set; }

    [Required(ErrorMessage = "配置内容不能为空")]
    public string Json { get; set; }
}
