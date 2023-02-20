using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Excel.Abstractions;
using CRB.TPM.Excel.Abstractions.Export;

namespace CRB.TPM.Excel.EPPlus
{
    public static class ExcelOptionsBuilderExtensions
    {
        /// <summary>
        /// 使用EPPlus
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ExcelOptionsBuilder UseEPPlus(this ExcelOptionsBuilder builder)
        {
            builder.Services.AddSingleton<IExcelProvider, EPPlusExcelProvider>();
            builder.Services.AddSingleton<IExcelExportBuilder, EPPlusExcelExportBuilder>();
            return builder;
        }
    }
}
