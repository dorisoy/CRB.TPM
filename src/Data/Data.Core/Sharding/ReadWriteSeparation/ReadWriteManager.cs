using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRB.TPM.Data.Abstractions.ReadWriteSeparation;

namespace CRB.TPM.Data.Core.ReadWriteSeparation;

/// <summary>
/// 用于表示读写分离管理器
/// </summary>
public class ReadWriteManager : IReadWriteManager
{
    private readonly IReadWriteAccessor _readWriteAccessor;

    public ReadWriteManager(IReadWriteAccessor readWriteAccessor)
    {
        _readWriteAccessor = readWriteAccessor;
    }

    /// <summary>
    /// 获取当前读写上下文
    /// </summary>
    /// <returns></returns>
    public ReadWriteContext GetCurrent()
    {
        return _readWriteAccessor.ReadWriteContext;
    }


    /// <summary>
    /// 创建读写上下文生命周期范围
    /// </summary>
    /// <returns></returns>
    public ReadWriteScope CreateScope()
    {
        var shardingPageScope = new ReadWriteScope(_readWriteAccessor);

        //创建上下文
        shardingPageScope.ReadWriteAccessor.ReadWriteContext = ReadWriteContext.Create();

        return shardingPageScope;
    }
}
