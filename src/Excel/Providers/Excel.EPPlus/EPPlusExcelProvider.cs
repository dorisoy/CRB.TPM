using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Module.Abstractions.Options;
using Microsoft.Extensions.Options;
using CRB.TPM.Excel.Abstractions.Export;
using CRB.TPM.Excel.Core;

namespace CRB.TPM.Excel.EPPlus;

internal class EPPlusExcelProvider : ExcelProviderAbstract
{
    public EPPlusExcelProvider(IAccount loginInfo, IExcelExportBuilder exportBuilder, IOptionsMonitor<ExcelOptions> options, IOptionsMonitor<CommonOptions> commonOptions) : base(options, commonOptions, exportBuilder)
    {
    }
}