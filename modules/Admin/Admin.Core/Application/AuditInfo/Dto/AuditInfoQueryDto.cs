using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Mod.Admin.Core.Domain.AuditInfo;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Auth.Abstractions;
using System;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Admin.Core.Domain.LoginLog;

namespace CRB.TPM.Mod.Admin.Core.Application.AuditInfo.Dto;

public class AuditInfoQueryDto : QueryDto
{
    /// <summary>
    /// 访问来源
    /// </summary>
    public int? Platform { get; set; }

    /// <summary>
    /// 模块
    /// </summary>
    public string ModuleCode { get; set; }

    /// <summary>
    /// 控制器
    /// </summary>
    public string Controller { get; set; }

    /// <summary>
    /// 方法
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }


    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<AuditInfoEntity> ExportModel { get; set; }

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
