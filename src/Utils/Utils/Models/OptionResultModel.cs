using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace CRB.TPM;

/// <summary>
/// 可选项返回模型
/// </summary>
public class OptionResultModel<T>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// 禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 扩展数据
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; set; }


    /// <summary>
    /// 扩展数据
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic Extends { get; set; }

    /// <summary>
    /// 子级
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<OptionResultModel<T>> Children { get; set; }
}

/// <summary>
/// 可选项返回模型
/// </summary>
public class OptionResultModel : OptionResultModel<object>
{
    /// <summary>
    /// 子级
    /// </summary>
    public new List<OptionResultModel> Children { get; set; }
}