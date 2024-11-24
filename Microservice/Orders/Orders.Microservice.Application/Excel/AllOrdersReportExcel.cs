using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using Orders.Microservice.Domain.Entities;
using System.Drawing;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Orders.Microservice.Application.Excel
{
    public static class AllOrdersReportExcel
    {
        public static async Task<byte[]> ProcessExcel(List<Order> orders)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();

            ProductsReport(package, orders);
            OrdersReport(package, orders);
            CatalogReport(package, orders);

            return await Task.FromResult(package.GetAsByteArray());
        }

        private static void ProductsReport(ExcelPackage package, List<Order> orders)
        {
            var worksheet = package.Workbook.Worksheets.Add("Отчёт по продуктам");

            worksheet.Cells[1, 1].Value = "#ID продукта";
            worksheet.Cells[1, 2].Value = "Наименование";
            worksheet.Cells[1, 3].Value = "Каталог";
            worksheet.Cells[1, 4].Value = "Общее кол-во в заказах";
            ExcelSupport.SetCenterTextToCell(worksheet.Cells[1, 1, 1, 4]);
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[1, 1, 1, 4], ExcelBorderStyle.Thin);
            ExcelSupport.ApplyCellFill(worksheet.Cells[1, 1, 1, 4], Color.LightSteelBlue);

            worksheet.Cells[2, 1].Value = "1";
            worksheet.Cells[2, 2].Value = "2";
            worksheet.Cells[2, 3].Value = "3";
            worksheet.Cells[2, 4].Value = "4";
            worksheet.Cells[2, 1, 2, 4].AutoFilter = true;
            ExcelSupport.SetCenterTextToCell(worksheet.Cells[2, 1, 2, 4]);
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[2, 1, 2, 4], ExcelBorderStyle.Thin);

            worksheet.View.FreezePanes(3, 5);

            int row = 3;
            var distinctOrders = orders.SelectMany(x => x.Items)
                .GroupBy(item => new { item.ProductId, item.ProductName, item.CatalogName })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    CatalogName = group.Key.CatalogName,
                    TotalQuantity = group.Sum(item => item.Quantity)
                })
                .OrderBy(x => x.ProductName)
                    .ThenBy(x => x.CatalogName)
                    .ThenBy(x => x.TotalQuantity)
                .ToList();
            foreach (var item in distinctOrders)
            {
                worksheet.Cells[row, 1].Value = item.ProductId;
                worksheet.Cells[row, 2].Value = item.ProductName;
                worksheet.Cells[row, 3].Value = item.CatalogName;
                worksheet.Cells[row, 4].Value = item.TotalQuantity;
                ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[row, 1, row, 4], ExcelBorderStyle.Thin);
                row++;
            }

            worksheet.Cells[row, 3].Value = "Итого:";
            worksheet.Cells[row, 4].Formula = $"SUM(D3:D{row - 1})";
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 3, row, 4], Color.PapayaWhip);

            worksheet.Cells[1, 1, row - 1, 4].AutoFitColumns();
            worksheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
        }

        private static void OrdersReport(ExcelPackage package, List<Order> orders)
        {
            var worksheet = package.Workbook.Worksheets.Add("Отчёт по заказам");
            var row = 1;
            foreach (var item in orders)
            {
                GenerateOrderTable(worksheet, item, ref row);
                row++;
            }
            worksheet.Cells[1, 1, row - 1, 4].AutoFitColumns();
        }

        private static void CatalogReport(ExcelPackage package, List<Order> orders)
        {
            var worksheet = package.Workbook.Worksheets.Add("Отчёт по категориям");
            var row = 1;

            worksheet.Cells[row, 1].Value = "Категория";
            worksheet.Cells[row, 2].Value = "Кол-во товаров в заказах";
            ExcelSupport.SetCenterTextToCell(worksheet.Cells[row, 1, row, 2]);
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[row, 1, row, 2], ExcelBorderStyle.Thick);
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 1, row, 2], Color.LightSteelBlue);
            row++;
            var items = orders.SelectMany(x => x.Items)
                .GroupBy(item => new { item.CatalogName })
                .Select(group => new
                {
                    CatalogName = group.Key.CatalogName,
                    TotalQuantity = group.Sum(item => item.Quantity)
                })
                .OrderBy(x => x.CatalogName)
                    .ThenBy(x => x.TotalQuantity)
                .ToList();
            foreach (var item in items)
            {
                worksheet.Cells[row, 1].Value = item.CatalogName;
                worksheet.Cells[row, 2].Value = item.TotalQuantity;
                ExcelSupport.ApplyBordersForRange(worksheet.Cells[row, 1, row, 2], left: ExcelBorderStyle.Thin, right: ExcelBorderStyle.Thin, bottom: ExcelBorderStyle.Thin);
                row++;
            }
            worksheet.Cells[1, 1, row - 1, 4].AutoFitColumns();

            var chart = worksheet.Drawings.AddChart("CatalogDistributionChart", eChartType.Pie);
            chart.SetPosition(1, 0, 3, 0);
            chart.SetSize(600, 400);
            var dataRange = worksheet.Cells[2, 2, row - 1, 2];
            var categoryRange = worksheet.Cells[2, 1, row - 1, 1];
            chart.Series.Add(dataRange, categoryRange);
            chart.Title.Text = "Распределение по категориям";
        }

        private static void GenerateOrderTable(ExcelWorksheet worksheet, Domain.Entities.Order order, ref int row)
        {
            worksheet.Cells[row, 1].Value = "ФИО клиента";
            worksheet.Cells[row, 2].Value = "ФИО ресурсного менеджера";
            worksheet.Cells[row, 3].Value = "Статус";
            worksheet.Cells[row, 4].Value = "Дата заказа";
            ExcelSupport.SetCenterTextToCell(worksheet.Cells[row, 1, row, 4]);
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[row, 1, row, 4], ExcelBorderStyle.Thick);
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 1, row, 4], Color.LightSteelBlue);
            row++;

            worksheet.Cells[row, 1].Value = order.CustomerFullname;
            worksheet.Cells[row, 2].Value = order.ManagerFullname;
            worksheet.Cells[row, 3].Value = GetStatusText(order.Status);
            worksheet.Cells[row, 4].Value = order.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            ExcelSupport.ApplyBordersForRange(worksheet.Cells[row, 1, row, 4], left: ExcelBorderStyle.Thin, right: ExcelBorderStyle.Thin, bottom: ExcelBorderStyle.Thin);
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 1, row, 4], Color.LightBlue);
            row++;

            worksheet.Cells[row, 1].Value = "Наименование";
            worksheet.Cells[row, 2].Value = "Каталог";
            worksheet.Cells[row, 3].Value = "Количество";
            ExcelSupport.SetCenterTextToCell(worksheet.Cells[row, 1, row, 3]);
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[row, 1, row, 3], ExcelBorderStyle.Thick);
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 1, row, 3], Color.Gainsboro);
            row++;

            var startProductsPoz = row;
            foreach (var item in order.Items)
            {
                worksheet.Cells[row, 1].Value = item.ProductName;
                worksheet.Cells[row, 2].Value = item.CatalogName;
                worksheet.Cells[row, 3].Value = item.Quantity;
                ExcelSupport.ApplyBordersForRange(worksheet.Cells[row, 1, row, 3], left: ExcelBorderStyle.Thin, right: ExcelBorderStyle.Thin, bottom: ExcelBorderStyle.Thin);
                ExcelSupport.ApplyCellFill(worksheet.Cells[row, 1, row, 3], Color.Silver);
                row++;
            }
            worksheet.Cells[row, 2].Value = "Итого:";
            if (order.Items.Count != 0) worksheet.Cells[row, 3].Formula = $"SUM(C{startProductsPoz}:C{row - 1})";
            ExcelSupport.SetCellRangeBorderAround(worksheet.Cells[row, 2, row, 3], ExcelBorderStyle.Thin);
            ExcelSupport.ApplyCellFill(worksheet.Cells[row, 2, row, 3], Color.PapayaWhip);
            row++;
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
