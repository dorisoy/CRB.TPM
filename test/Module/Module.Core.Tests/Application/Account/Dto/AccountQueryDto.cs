using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Mod.Module.Core.Domain.Account;

namespace CRB.TPM.Mod.Module.Core.Application.Account.Dto;

public class AccountQueryDto : QueryDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }


    #region ExcelExport 导出信息

    /// <summary>
    /// 导出信息
    /// </summary>
    public ExcelExportEntityModel<AccountEntity> ExportModel { get; set; } = new();

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
