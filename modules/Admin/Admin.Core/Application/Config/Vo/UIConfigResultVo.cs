using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Config.Core;

namespace CRB.TPM.Mod.Admin.Core.Application.Config.Vo;

/// <summary>
/// 前端UI配置返回模型
/// </summary>
public class UIConfigResultVo
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public UISystemVo System { get; set; }

    /// <summary>
    /// 权限验证
    /// </summary>
    public UIPermissionVo Permission { get; set; }

    /// <summary>
    /// 组件配置
    /// </summary>
    public ComponentConfig Component { get; set; }
}

/// <summary>
/// UI系统信息
/// </summary>
public class UISystemVo
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Logo
    /// </summary>
    public string Logo { get; set; }

    /// <summary>
    /// 版权声明
    /// </summary>
    public string Copyright { get; set; }
}

/// <summary>
/// UI权限验证
/// </summary>
public class UIPermissionVo
{
    /// <summary>
    /// 开启权限验证
    /// </summary>
    public bool Validate { get; set; }

    /// <summary>
    /// 开启按钮权限
    /// </summary>
    public bool Button { get; set; }
}
