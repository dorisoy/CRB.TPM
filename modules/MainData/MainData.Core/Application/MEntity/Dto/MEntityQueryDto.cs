using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MEntity;
using CRB.TPM.Excel.Abstractions.Export;

namespace CRB.TPM.Mod.MainData.Core.Application.MEntity.Dto;

/// <summary>
/// 业务实体查询模型
/// </summary>
public class MEntityQueryDto : QueryDto
{
    /// <summary>
    /// 同时支持法人编码/法人名称查询	
    /// </summary>
    public string Name { get; set; }

    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<MEntityExportDto> ExportModel { get; set; } 

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