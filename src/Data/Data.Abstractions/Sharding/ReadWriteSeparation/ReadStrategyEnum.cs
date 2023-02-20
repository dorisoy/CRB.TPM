using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.ReadWriteSeparation;

/// <summary>
/// 节点类型
/// </summary>
[Flags]
public enum NodeType
{
    /// <summary>
    /// 主节点
    /// </summary>
    Master = 1,
    /// <summary>
    /// 从节点
    /// </summary>
    Slave = 2
}

/// <summary>
/// 读取策略
/// </summary>
public enum ReadStrategyEnum
{
    /// <summary>
    /// 表示针对同一个数据源获取链接采用随机策略,（可以设置同一个链接多次就是所谓的权重）
    /// </summary>
    Random = 1,
    /// <summary>
    /// 表示同一个数据源的从库链接读取策略为轮询一个接一个公平读取,（可以设置同一个链接多次就是所谓的权重）
    /// </summary>
    Loop = 2,
}

/// <summary>
/// 连接字符串读取策略
/// </summary>
public enum ReadConnStringGetStrategyEnum
{
    /// <summary>
    /// 每次都是最新的：表示每一次查询都是获取一次从库，但是可能会出现比如page的两次操作count+list结果和实际获取的不一致等情况，
    /// 大部分情况下不会出现问题只是有可能会出现这种情况
    /// </summary>
    LatestEveryTime,
    /// <summary>
    /// 已dbcontext作为缓存条件每次都是第一次获取的：表示针对同一个dbcontext只取一次从库链接，
    /// 保证同一个dbcontext下的从库链接都是一样的，不会出现说查询主表数据存在但是第二次查询可能走的是其他从库导致明细表不存在等问题，
    /// 所以建议大部分情况下使用LatestFirstTime
    /// </summary>
    LatestFirstTime
}
