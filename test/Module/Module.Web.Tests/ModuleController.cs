using System;
using Microsoft.AspNetCore.Mvc;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Module.Web;
using CRB.TPM;

namespace CRB.TPM.Mod.Module.Web;

[Area("Module")]
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