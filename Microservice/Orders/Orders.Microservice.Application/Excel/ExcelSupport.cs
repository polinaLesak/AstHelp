using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Orders.Microservice.Application.Excel
{
    public static class ExcelSupport
    {
        public static void SetCenterTextToCell(ExcelRange cells)
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        public static void SetCellRangeBorderAround(ExcelRange range, ExcelBorderStyle style)
        {
            foreach (var cell in range)
            {
                cell.Style.Border.BorderAround(style);
            }
        }

        public static void ApplyBorders(ExcelRangeBase cell,
                                    ExcelBorderStyle? top = null,
                                    ExcelBorderStyle? bottom = null,
                                    ExcelBorderStyle? left = null,
                                    ExcelBorderStyle? right = null)
        {
            var border = cell.Style.Border;

            if (top.HasValue)
                border.Top.Style = top.Value;

            if (bottom.HasValue)
                border.Bottom.Style = bottom.Value;

            if (left.HasValue)
                border.Left.Style = left.Value;

            if (right.HasValue)
                border.Right.Style = right.Value;
        }

        public static void ApplyBordersForRange(ExcelRange range,
                                    ExcelBorderStyle? top = null,
                                    ExcelBorderStyle? bottom = null,
                                    ExcelBorderStyle? left = null,
                                    ExcelBorderStyle? right = null)
        {
            foreach (var cell in range)
            {
                ApplyBorders(cell, top: top, bottom: bottom, left: left, right: right);
            }
        }

        public static void ApplyCellFill(ExcelRange cells, System.Drawing.Color fillColor)
        {
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(fillColor);
        }
    }
}
