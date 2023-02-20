using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRB.TPM;

/// <summary>
/// 树形结构返回模型
/// </summary>
/// <typeparam name="TKey">编号类型</typeparam>
/// <typeparam name="T"></typeparam>
public class TreeResultModel<TKey, T> where T : class, new()
{
    /// <summary>
    /// 编号
    /// </summary>
    public TKey Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 值(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object ParentId { get; set; }

    /// <summary>
    /// 值(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Value { get; set; }

    /// <summary>
    /// 扩展属性(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Level { get; set; }

    /// <summary>
    /// 数据项
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Item { get; set; }

    /// <summary>
    /// 路径列表
    /// </summary>
    [SwaggerIgnore]
    public List<string> Path { get; set; } = new();

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeResultModel<TKey, T>> Children { get; set; } = new();
}

/// <summary>
/// 树形结构返回模型
/// </summary>
/// <typeparam name="T"></typeparam>
public class TreeResultModel<T> : TreeResultModel<Guid, T> where T : class, new()
{
    /// <summary>
    /// 子节点
    /// </summary>
    public new List<TreeResultModel<T>> Children { get; set; } = new();
}


/// <summary>
/// 树形结构返回模型
/// </summary>
/// <typeparam name="TKey">编号类型</typeparam>
/// <typeparam name="T"></typeparam>
public class TreeResultModel2<TKey, T> where T : class, new()
{
    /// <summary>
    /// 编号
    /// </summary>
    public TKey Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 值(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object ParentId { get; set; }

    /// <summary>
    /// 值(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Value { get; set; }

    /// <summary>
    /// 扩展属性(可选)
    /// </summary>
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Level { get; set; }

    /// <summary>
    /// 数据项
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Item { get; set; }

    /// <summary>
    /// 路径列表
    /// </summary>
    [SwaggerIgnore]
    public List<string> Path { get; set; } = new();

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeResultModel<TKey, T>> Children { get; set; } = new();
}

