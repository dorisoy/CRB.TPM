using System;
using CRB.TPM.Data.Abstractions.Descriptors;

namespace CRB.TPM.Data.Abstractions.Options;

/// <summary>
/// 代码优先配置项
/// </summary>
public class CodeFirstOptions
{
    /// <summary>
    /// 自定义代码优先提供器
    /// </summary>
    public ICodeFirstProvider CustomCodeFirstProvider { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool CodeFirst { get; set; }

    /// <summary>
    /// 是否创建库
    /// </summary>
    public bool CreateDatabase { get; set; }

    /// <summary>
    /// 是否更新列
    /// </summary>
    public bool UpdateColumn { get; set; }

    /// <summary>
    /// 是否初始化数据
    /// </summary>
    public bool InitData { get; set; }

    /// <summary>
    /// 是否启用分表策略（如：已经在实体启用 [Sharding(ShardingPolicy.Month)]，而实际不需要创建分表的场景下非常有用）
    /// </summary>
    public bool EnableShardingPolicy { get; set; } = true;

    /// <summary>
    /// 初始化数据的文件路径
    /// </summary>
    public string InitDataFilePath { get; set; }

    /// <summary>
    /// 创建数据库前事件
    /// </summary>
    public Action<IDbContext> BeforeCreateDatabase { get; set; }

    /// <summary>
    /// 创建数据库后事件
    /// </summary>
    public Action<IDbContext> AfterCreateDatabase { get; set; }

    /// <summary>
    /// 创建表前事件
    /// </summary>
    public Action<IDbContext, IEntityDescriptor> BeforeCreateTable { get; set; }

    /// <summary>
    /// 创建表后事件
    /// </summary>
    public Action<IDbContext, IEntityDescriptor> AfterCreateTable { get; set; }
}