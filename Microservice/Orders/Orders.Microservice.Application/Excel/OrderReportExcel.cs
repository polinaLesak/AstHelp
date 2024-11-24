using OfficeOpenXml;
using OfficeOpenXml.Style;
using Orders.Microservice.Domain.Entities;
using System.Drawing;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Orders.Microservice.Application.Excel
{
    public static class OrderReportExcel
    {
        public static async Task<byte[]> ProcessExcel(Order order)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Отчёт по заказу");

            worksheet.Cells[1, 1].Value = "#ID заказа";
            worksheet.Cells[1, 2].Value = "ФИО клиента";
            worksheet.Cells[1, 3].Value = "ФИО ресурсного менеджера";
            worksheet.Cells[1, 4].Value = "Причина выдачи";
            worksheet.Cells[1, 5].Value = "Создано";
            worksheet.Cells[1, 6].Value = "Статус";
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[1, 1, 1, 6], ExcelBorderStyle.Thick);
            ExcelSupport.ApplyCellFill(worksheet.Cells[1, 1, 1, 6], Color.LightSteelBlue);
            worksheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;

            worksheet.Cells[2, 1].Value = order.Id;
            worksheet.Cells[2, 2].Value = order.CustomerFullname;
            worksheet.Cells[2, 3].Value = order.ManagerFullname;
            worksheet.Cells[2, 4].Value = order.ReasonForIssue;
            worksheet.Cells[2, 5].Value = order.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            worksheet.Cells[2, 6].Value = GetStatusText(order.Status);
            ExcelSupport.ApplyBordersForRange(worksheet.Cells[2, 1, 2, 6], left: ExcelBorderStyle.Thin, right: ExcelBorderStyle.Thin, bottom: ExcelBorderStyle.Thin);

            var itemsHeaderRow = 4;
            worksheet.Cells[itemsHeaderRow, 1].Value = "#ID продукта";
            worksheet.Cells[itemsHeaderRow, 2].Value = "Наименование";
            worksheet.Cells[itemsHeaderRow, 3].Value = "Каталог";
            worksheet.Cells[itemsHeaderRow, 4].Value = "Количество";
            worksheet.Cells[itemsHeaderRow, 1, itemsHeaderRow, 4].Style.Font.Bold = true;
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[itemsHeaderRow, 1, itemsHeaderRow, 4], ExcelBorderStyle.Thick);
            ExcelSupport.ApplyCellFill(worksheet.Cells[itemsHeaderRow, 1, itemsHeaderRow, 4], Color.LightSteelBlue);

            int row = itemsHeaderRow + 1;
            foreach (var item in order.Items)
            {
                worksheet.Cells[row, 1].Value = item.Id;
                worksheet.Cells[row, 2].Value = item.ProductName;
                worksheet.Cells[row, 3].Value = item.CatalogName;
                worksheet.Cells[row, 4].Value = item.Quantity;
                ExcelSupport.ApplyBordersForRange(worksheet.Cells[row, 1, row, 4], left: ExcelBorderStyle.Thin, right: ExcelBorderStyle.Thin, bottom: ExcelBorderStyle.Thin);
                row++;
            }

            worksheet.Cells[1, 1, row - 1, 4].AutoFitColumns();

            return await Task.FromResult(package.GetAsByteArray());
        }

        private static string GetStatusText(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Ожидание",
                OrderStatus.Processing => "В обработке",
                OrderStatus.Packaged => "Укомплектован",
                OrderStatus.Performed => "Выполнен",
                OrderStatus.Canceled => "Отменён",
                _ => "Неизвестно"
            };
        }
    }
}
