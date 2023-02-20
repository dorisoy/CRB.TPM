using CRB.TPM.Data.Abstractions.Options;
using System;

namespace CRB.TPM.Data.Abstractions.Logger;

/// <summary>
/// 数据库日志记录器
/// </summary>
public class DTPger
{
    private readonly DbOptions _options;
    private readonly IDTPgerProvider _provider;

    public DTPger(DbOptions options, IDTPgerProvider provider)
    {
        _options = options;
        _provider = provider;
    }

    /// <summary>
    /// 日志记录
    /// </summary>
    /// <param name="action">操作类型</param>
    /// <param name="sql">SQL语句</param>
    public void Write(string action, string sql)
    {
        if (_options.Log)
        {
            _provider.Write(action, sql);
        }
    }


    /// <summary>
    /// 日志记录
    /// </summary>
    /// <param name="action">操作类型</param>
    /// <param name="ex">异常</param>
    public void Write(string action, Exception ex)
    {
        if (_options.Log)
        {
            _provider.Write(action, ex);
        }
    }
}