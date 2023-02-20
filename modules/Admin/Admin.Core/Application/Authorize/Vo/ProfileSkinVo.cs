namespace CRB.TPM.Mod.Admin.Core.Application.Authorize.Vo;

/// <summary>
/// 个人信息皮肤
/// </summary>
public class ProfileSkinVo
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 主题
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    /// 尺寸
    /// </summary>
    public string Size { get; set; }
}


/// <summary>
/// 标签导航组件配置信息
/// </summary>
public class TabnavVo
{
    /// <summary>
    /// 是否启用标签导航
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 是否显示首页
    /// </summary>
    public bool ShowHome { get; set; } = true;

    /// <summary>
    /// 首页地址
    /// </summary>
    public string HomeUrl { get; set; }

    /// <summary>
    /// 是否显示图标
    /// </summary>
    public bool ShowIcon { get; set; } = true;

    /// <summary>
    /// 最大页面数量
    /// </summary>
    public int MaxOpenCount { get; set; } = 20;
}