using System;
using Microsoft.AspNetCore.Mvc;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Module.Web;
using CRB.TPM.Auth.Abstractions.Annotations;


namespace CRB.TPM.Mod.Scheduler.Web;

[Area("Scheduler")]
//要启用审计功能，请注释DisableAudit特性
[DisableAudit]
public abstract class ModuleController : ControllerAbstract
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    protected new IActionResult ExportExcel(ExcelModel model)
    {
        if (model.FileName.IsNull())
        {
            model.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        return PhysicalFile(model.StoragePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", model.FileName, true);
    }
}