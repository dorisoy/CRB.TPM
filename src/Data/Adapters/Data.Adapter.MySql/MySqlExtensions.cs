using System;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.Options;


namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 使用MySql数据库
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="configure">自定义配置</param>
    /// <returns></returns>
    public static IDbBuilder UseMySql(this IDbBuilder builder, string connectionString, Action<DbOptions> configure = null)
    {
        builder.UseDb(connectionString, DbProvider.MySql, configure);

        return builder;
    }
}