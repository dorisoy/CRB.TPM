using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;
using CRB.TPM.Utils.Annotations;
using System;

namespace CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo.Dto;

[ObjectMap(typeof(AuditInfoEntity))]
public class AuditInfoAddDto
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 账户名称
    /// </summary>
    public string AccountName { get; set; }

    /// <summary>
    /// 区域(模块编码)
    /// </summary>
    public string Area { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 控制器
    /// </summary>
    public string Controller { get; set; }

    /// <summary>
    /// 控制器描述
    /// </summary>
    public string ControllerDesc { get; set; }

    /// <summary>
    /// 操作
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    public string ActionDesc { get; set; }

    /// <summary>
    /// 参数(Json序列化)
    /// </summary>
    public string Parameters { get; set; }

    /// <summary>
    /// 返回值(Json序列化)
    /// </summary>
    public string Result { get; set; }

    /// <summary>
    /// 方法开始执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }

    /// <summary>
    /// 方法执行总用时(ms)
    /// </summary>
    public long ExecutionDuration { get; set; }

    /// <summary>
    /// 浏览器信息
    /// </summary>
    public string BrowserInfo { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public int Platform { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string IP { get; set; }
}
