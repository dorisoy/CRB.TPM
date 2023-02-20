using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Admin.Core.Domain.AuditInfo;

/// <summary>
/// 审计信息
/// </summary>
[Table("AuditInfo")]
public partial class AuditInfoEntity : Entity<long>
{
    /// <summary>
    /// 账户编号
    /// </summary>
    [Nullable]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 账户名称
    /// </summary>
    [Nullable]
    public string AccountName { get; set; }

    /// <summary>
    /// 区域(模块编码)
    /// </summary>
    [Nullable]
    public string Area { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    [Nullable]
    public string Module { get; set; }

    /// <summary>
    /// 控制器
    /// </summary>
    public string Controller { get; set; }

    /// <summary>
    /// 控制器描述
    /// </summary>
    [Nullable]
    public string ControllerDesc { get; set; }

    /// <summary>
    /// 操作
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    [Nullable]
    public string ActionDesc { get; set; }

    /// <summary>
    /// 参数(Json序列化)
    /// </summary>
    [Nullable]
    [Length(4000)]
    public string Parameters { get; set; }

    /// <summary>
    /// 返回值(Json序列化)
    /// </summary>
    [Nullable]
    [Length(4000)]
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
    [Length(500)]
    [Nullable]
    public string BrowserInfo { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public int Platform { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Nullable]
    public string IP { get; set; }
}
