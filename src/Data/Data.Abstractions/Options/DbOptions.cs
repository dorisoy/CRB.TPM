using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CRB.TPM.Data.Abstractions.Options;

/// <summary>
/// 用于表示数据库配置项
/// </summary>
public class DbOptions
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 读写分离配置项
    /// </summary>
    public ReadWriteSeparationOptions ReadWriteSeparationOptions { get; set; }

    /// <summary>
    /// 是否使用读写分离
    /// </summary>
    /// <returns></returns>
    public bool UseReadWriteSeparation { get; set; } = false;

    /// <summary>
    /// 是否启用客户端模式
    /// </summary>
    /// <returns></returns>
    public bool UseClientMode { get; set; } = false;

    /// <summary>
    /// 数据库提供器
    /// </summary>
    public DbProvider Provider { get; set; }

    /// <summary>
    /// 是否开启日志
    /// </summary>
    public bool Log { get; set; }

    /// <summary>
    /// 数据库版本
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 表名称前缀
    /// </summary>
    public string TableNamePrefix { get; set; }

    /// <summary>
    /// 表名称分隔符
    /// </summary>
    public string TableNameSeparator { get; set; } = "_";

    /// <summary>
    /// 模块编码(CRB.TPM专属属性)
    /// </summary>
    public string ModuleCode { get; set; }

}


public static class DbOptionsExt
{
    public static DbOptions UseReadWriteSeparation(this DbOptions dbOptions, Action<ReadWriteSeparationOptions> configure)
    {
        var options = new ReadWriteSeparationOptions();
        configure?.Invoke(options);

        dbOptions.ReadWriteSeparationOptions = options;

        return dbOptions;
    }
}