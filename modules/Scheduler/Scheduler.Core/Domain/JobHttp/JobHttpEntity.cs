using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Enums;

namespace CRB.TPM.Mod.Scheduler.Core.Domain.JobHttp;

/// <summary>
/// Http任务信息
/// </summary>
[Table("Job_Http")]
public partial class JobHttpEntity : EntityBase<Guid>
{
    /// <summary>
    /// 任务编号
    /// </summary>
    public Guid JobId { get; set; }

    /// <summary>
    /// 请求Url
    /// </summary>
    [Nullable]
    [Length(500)]
    public string Url { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// 认证方式
    /// </summary>
    public AuthType AuthType { get; set; }

    /// <summary>
    /// 令牌
    /// </summary>
    [Nullable]
    [Length(300)]
    public string Token { get; set; }

    /// <summary>
    /// 数据格式
    /// </summary>
    public ContentType ContentType { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    [MaxLength]
    [Nullable]
    public string Parameters { get; set; }

    /// <summary>
    /// 请求头(json格式)
    /// </summary>
    [MaxLength]
    [Nullable]
    public string Headers { get; set; }
}
