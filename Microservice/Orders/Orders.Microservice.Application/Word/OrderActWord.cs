using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Orders.Microservice.Application.DTOs;

namespace Orders.Microservice.Application.Word
{
    public static class OrderActWord
    {
        public static async Task<byte[]> ProcessWord(Stream templateStream, OrderActDto dto)
        {
            var outputPath = Path.Combine(Path.GetTempPath(), $"Act_{dto.Order.Id}.docx");

            using (var outputStream = File.Create(outputPath))
            {
                templateStream.CopyTo(outputStream);
            }

            using (var document = WordprocessingDocument.Open(outputPath, true))
            {
                var body = document.MainDocumentPart.Document.Body;

                ReplacePlaceholder(body, "adminFullname", dto.AdminFullname);
                ReplacePlaceholder(body, "customerFullname", dto.CustomerFullname);
                ReplacePlaceholder(body, "adminPosition", dto.AdminFullname);
                ReplacePlaceholder(body, "customerPosition", dto.CustomerFullname);

                var table = body.Elements<Table>().FirstOrDefault();
                if (table != null)
                {
                    int index = 1;
                    foreach (var item in dto.Order.Items)
                    {
                        var row = new TableRow();
                        AppendTableCell(row, index.ToString());
                        AppendTableCell(row, $"{item.CatalogName}, {item.ProductName}" );
                        AppendTableCell(row, "шт.");
                        AppendTableCell(row, item.Quantity.ToString());
                        table.Append(row);
                        index++;
                    }
                }

                document.Save();
            }

            return await File.ReadAllBytesAsync(outputPath);            
        }

        private static void AppendTableCell(TableRow row, string value)
        {
            var cell = new TableCell(new Paragraph(new Run(new Text(value))));
            var run = cell.Descendants<Run>().FirstOrDefault();
            if (run != null)
            {
                var runProperties = new RunProperties();
                runProperties.FontSize = new FontSize() { Val = new StringValue((14 * 2).ToString()) };
                runProperties.RunFonts = new RunFonts() { Ascii = "Times New Roman" };
                run.PrependChild(runProperties);
            }
            row.Append(cell);
        }

        private static void ReplacePlaceholder(Body body, string placeholder, string value)
        {
            foreach (var text in body.Descendants<Text>().Where(t => t.Text.Contains(placeholder)))
            {
                text.Text = text.Text.Replace(placeholder, value);
            }
        }
    }
}
