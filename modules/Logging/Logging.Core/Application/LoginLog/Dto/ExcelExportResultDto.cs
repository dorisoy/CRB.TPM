using CRB.TPM.Excel.Abstractions.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Mod.Logging.Core.Domain.LoginLog;

namespace CRB.TPM.Mod.Logging.Core.Application.LoginLog.Dto
{
    //ExcelExportEntityModel
    public class ExcelExportResultDto: ExcelExportEntityModel<LoginLogEntity>
    {
    }
}
