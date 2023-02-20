using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Json;

namespace CRB.TPM.Mod.Admin.Core.Domain.Menu;

public partial class MenuEntity
{
    [NotMappingColumn]
    public Guid MenuId { get; set; } 

    /// <summary>
    /// 类型名称
    /// </summary>
    [Ignore]
    public string TypeName => Type.ToDescription();

    /// <summary>
    /// 打开方式名称
    /// </summary>
    [Ignore]
    public string OpenTargetName => OpenTarget.ToDescription();

    /// <summary>
    /// 多语言配置
    /// </summary>
    public MenuLocales Locales
    {
        get
        {
            if (LocalesConfig.NotNull())
            {
                return JsonHelper.Instance.Deserialize<MenuLocales>(LocalesConfig);
            }

            return new MenuLocales();
        }
    }
}

public class MenuLocales
{
    /// <summary>
    /// 中文
    /// </summary>
    [JsonPropertyName("zh-cn")]
    public string ZhCN { get; set; }

    /// <summary>
    /// 英文
    /// </summary>
    public string En { get; set; }
}