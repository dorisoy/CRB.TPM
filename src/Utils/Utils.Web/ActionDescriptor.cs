using System.Reflection;
using System.Text.Json.Serialization;

namespace CRB.TPM.Utils.Web;

/// <summary>
/// 操作描述符
/// </summary>
public class ActionDescriptor
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 控制器
    /// </summary>
    public ControllerDescriptor Controller { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 方法信息
    /// </summary>
    [JsonIgnore]
    public MethodInfo MethodInfo { get; set; }
}