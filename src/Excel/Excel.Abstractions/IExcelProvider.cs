using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Export;

namespace CRB.TPM.Excel.Abstractions;

/// <summary>
/// Excel提供器接口
/// </summary>
public interface IExcelProvider
{
    /// <summary>
    /// 导出实体数据
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<IResultModel<ExcelModel>> Export<T>(ExcelExportEntityModel<T> model) where T : IEntity;

    /// <summary>
    /// 根据模板导出
    /// </summary>
    /// <param name="templatePath"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    IResultModel<ExcelModel> ExportByTemplate(string templatePath, IList<Dictionary<string, object>> properties);
}