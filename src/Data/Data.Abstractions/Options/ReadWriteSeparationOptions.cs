using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Abstractions.Options;

/// <summary>
/// 读写分离配置
/// </summary>
public class ReadWriteSeparationOptions
{
    /// <summary>
    /// 主节点配置
    /// </summary>
    public Node[] Master { get; set; }

    /// <summary>
    /// 从节点配置
    /// </summary>
    public Node[] Slave { get; set; }

    /// <summary>
    /// 读取策略
    /// </summary>
    public ReadStrategyEnum ReadStrategy { get; set; } = ReadStrategyEnum.Loop;

    /// <summary>
    /// 是否默认启用
    /// </summary>
    public bool DefaultEnable { get; set; } = false;

    /// <summary>
    /// 默认策略
    /// </summary>
    public int DefaultPriority { get; set; } = 10;

    /// <summary>
    /// 连接字符串读取策略
    /// </summary>
    public ReadConnStringGetStrategyEnum ReadConnStringGetStrategy { get; set; } = ReadConnStringGetStrategyEnum.LatestFirstTime;
}
