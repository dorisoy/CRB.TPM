using CRB.TPM.Auth.Abstractions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Excel.Abstractions.Export;

namespace CRB.TPM.Excel.EPPlus;

internal class EPPlusExcelExportBuilder : IExcelExportBuilder
{
    private readonly IAccount _account;

    public EPPlusExcelExportBuilder(IAccount account)
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        _account = account;
    }

    public async Task Build<T>(ExcelExportEntityModel<T> model, Stream stream) where T : IEntity
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add(model.Title);

            var index = new SheetRowIndex();

            SetTitle(worksheet, model, index);

            SetDescription(worksheet, model, index);

            SetColumnName(worksheet, model.ShowColumnName, model.Columns, index);

            SetColumn(worksheet, model.Columns, model.Entities, index);

            worksheet.Cells.AutoFitColumns();

            await package.SaveAsAsync(stream);
        }
    }

    /*
     System.ObjectDisposedException: Cannot access a disposed object.\r\nObject name: 'The stream with Id 20c52d70-3273-475e-b5e6-9e3c2c19b4bf and Tag  is disposed.'.\r\n   at Microsoft.IO.RecyclableMemoryStream.ThrowDisposedException() in /_/src/RecyclableMemoryStream.cs:line 1437\r\n   at Microsoft.IO.RecyclableMemoryStream.SafeRead(Byte[] buffer, Int32 offset, Int32 count, Int64& streamPosition) in /_/src/RecyclableMemoryStream.cs:line 893\r\n   at Microsoft.IO.RecyclableMemoryStream.Read(Byte[] buffer, Int32 offset, Int32 count) in /_/src/RecyclableMemoryStream.cs:line 851\r\n   at System.IO.MemoryStream.ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)\r\n--- End of stack trace from previous location ---\r\n   at OfficeOpenXml.ExcelPackage.CopyStreamAsync(Stream inputStream, Stream outputStream, CancellationToken cancellationToken)\r\n   at OfficeOpenXml.ExcelPackage.SaveAsAsync(Stream OutputStream, CancellationToken cancellationToken)\r\n   at CRB.TPM.Excel.Core.ExcelProviderAbstract.Export[T](ExcelExportEntityModel`1 model) in D:\\Git\\CRB\\CRB.TPM\\src\\Excel\\Excel.Core\\ExcelProviderAbstract.cs:line 101\r\n   at CRB.TPM.Excel.Core.ExcelProviderAbstract.Export[T](ExcelExportEntityModel`1 model) in D:\\Git\\CRB\\CRB.TPM\\src\\Excel\\Excel.Core\\ExcelProviderAbstract.cs:line 105\r\n   at CRB.TPM.Mod.Module.Core.Application.Account.AccountService.Export(AccountQueryDto dto) in D:\\Git\\CRB\\CRB.TPM\\test\\Module\\Module.Core.Tests\\Application\\Account\\AccountService.cs:line 95\r\n   at CRB.TPM.Mod.Module.Web.Controllers.ExportController.Export(Int32 exportCount) in D:\\Git\\CRB\\CRB.TPM\\test\\Module\\Module.Web.Tests\\Controllers\\ExportController.cs:line 73\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.TaskOfIActionResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Logged|12_1(ControllerActionInvoker invoker)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResourceFilter>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeFilterPipelineAsync()\r\n--- End of stack trace from previous location ---\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)\r\n   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)\r\n   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Localization.RequestLocalizationMiddleware.Invoke(HttpContext context)\r\n   at CRB.TPM.Host.Web.Middleware.ExceptionHandleMiddleware.InvokeAsync(HttpContext httpContext) in D:\\Git\\CRB\\CRB.TPM\\src\\Host\\Host.Web\\Middleware\\ExceptionHandleMiddleware.cs:line 30
     */

    /// <summary>
    /// 设置标题
    /// </summary>
    private void SetTitle<T>(ExcelWorksheet sheet, ExcelExportEntityModel<T> model, SheetRowIndex index) where T : IEntity
    {
        if (!model.ShowTitle)
            return;

        sheet.Row(index.Next).Height = 30;
        var title = sheet.Cells[1, 1, 1, model.Columns.Count];
        title.Value = model.Title;
        title.Merge = true;
        title.Style.Font.Size = 17;
        title.Style.Font.Color.SetColor(Color.Black);
        title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        title.Style.Fill.PatternType = ExcelFillStyle.Solid;
        title.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));

        index.Next++;
    }

    /// <summary>
    /// 设置说明
    /// </summary>
    private void SetDescription<T>(ExcelWorksheet sheet, ExcelExportEntityModel<T> model, SheetRowIndex index) where T : IEntity
    {
        var subSb = new StringBuilder();
        if (model.ShowExportPeople)
        {
            subSb.AppendFormat("导出人：{0}    ", _account.AccountName);
        }

        if (model.ShowExportDate)
        {
            subSb.AppendFormat("导出时间：{0}    ", DateTime.Now.Format());
        }

        if (model.ShowCopyright)
        {
            subSb.AppendFormat("版权所有：{0}", model.Copyright);
        }

        if (subSb.Length < 1)
            return;

        sheet.Row(index.Next).Height = 20;
        var cell = sheet.Cells[index.Next, 1, 2, model.Columns.Count];
        cell.Value = subSb.ToString();
        cell.Merge = true;
        cell.Style.Font.Size = 10;
        cell.Style.Font.Color.SetColor(Color.Black);
        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 224, 180));

        index.Next++;
    }

    /// <summary>
    /// 设置列名
    /// </summary>
    private void SetColumnName(ExcelWorksheet sheet, bool showColumnName, IList<ExcelExportColumnModel> columns, SheetRowIndex index)
    {
        if (!showColumnName)
            return;

        sheet.Row(index.Next).Height = 25;
        for (int i = 0; i < columns.Count; i++)
        {
            var col = columns[i];
            var cell = sheet.Cells[index.Next, i + 1];
            cell.Value = col.Label;
            cell.Style.Font.Size = 12;
            cell.Style.Font.Bold = true;
            cell.Style.Font.Color.SetColor(Color.CornflowerBlue);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        index.Next++;
    }

    /// <summary>
    /// 设置列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sheet"></param>
    /// <param name="columns"></param>
    /// <param name="entities"></param>
    /// <param name="index"></param>
    private void SetColumn<T>(ExcelWorksheet sheet, IList<ExcelExportColumnModel> columns, IList<T> entities, SheetRowIndex index) where T : IEntity
    {
        foreach (var entity in entities)
        {
            sheet.Row(index.Next).Height = 20;

            for (int i = 0; i < columns.Count; i++)
            {
                var col = columns[i];
                var cell = sheet.Cells[index.Next, i + 1];
                cell.Style.Font.Size = 11;
                cell.Style.Font.Color.SetColor(Color.Black);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                if (col.PropertyInfo == null)
                {
                    cell.Value = "";
                }
                else
                {
                    var type = col.PropertyInfo.PropertyType;
                    if (col.PropertyInfo.PropertyType.IsNullable())
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }

                    if (type.IsDateTime())
                    {
                        cell.Style.Numberformat.Format = "yyyy/MM/dd HH:mm:ss";
                        cell.Formula = "=DATE(2014,10,5)";
                        //格式化
                        if (col.Format.NotNull())
                        {
                            cell.Style.Numberformat.Format = col.Format;
                        }
                    }

                    cell.Value = col.PropertyInfo.GetValue(entity);
                }
            }

            index.Next++;
        }
    }
}

internal class SheetRowIndex
{
    public int Next { get; set; } = 1;
}