using CRB.TPM.CAP.Abstractions;

namespace CRB.TPM.CAP.Abstractions;

/// <summary>
/// CAP操作配置项
/// </summary>
public class CAPOptions
{
    /// <summary>
    ///CAP Data 提供程序
    /// </summary>
    public CAPProvider Provider { get; set; }

    /// <summary>
    /// Data ConnectionString
    /// </summary>
    public string ConnectionStrings { get; set; }

    /// <summary>
    ///CAP MQ 提供程序
    /// </summary>
    public CAPMQProvider MQProvider { get; set; }

    /// <summary>
    /// MQ ConnectionString
    /// </summary>
    public string MQConnectionStrings { get; set; }
}