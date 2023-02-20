using CRB.TPM.Utils.Annotations;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Auth.Abstractions;
using System;
using CRB.TPM.Excel.Abstractions.Export;

namespace CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto;

public class LoginLogQueryDto : QueryDto
{
    /// <summary>
    /// 账户编号
    /// </summary>
    public Guid? AccountId { get; set; }

    /// <summary>
    /// 登录平台
    /// </summary>
    public int? Platform { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public LoginMode? LoginMode { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? EndDate { get; set; }


    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<LoginLogEntity> ExportModel { get; set; }

    /// <summary>
    /// 导出数量
    /// </summary>
    public long ExportCount { get; set; }

    /// <summary>
    /// 导出数量限制
    /// </summary>
    public virtual int ExportCountLimit => 50000;

    /// <summary>
    /// 是否超出导出数量限制
    /// </summary>
    public bool IsOutOfExportCountLimit => ExportCount > ExportCountLimit;


    /// <summary>
    /// 是否是导出操作
    /// </summary>
    public bool IsExport => ExportModel != null;

    /// <summary>
    /// 查询数量
    /// </summary>
    public bool QueryCount { get; set; } = true;

    #endregion
}
