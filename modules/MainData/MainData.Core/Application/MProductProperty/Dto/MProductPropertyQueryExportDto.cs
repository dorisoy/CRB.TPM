using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductProperty.Dto;

/// <summary>
/// 产品属性查询模型
/// </summary>
public class MProductPropertyQueryExportDto : MProductPropertyQueryDto
{
    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<MProductPropertyEntity> ExportModel { get; set; } = new ExcelExportEntityModel<MProductPropertyEntity>();

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