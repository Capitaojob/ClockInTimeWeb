using ClockInTimeWeb.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

namespace ClockInTimeWeb.Controllers
{
    public class PdfController : Controller
    {
        // GET: /PDF/GetPdf
        [HttpGet]
        public IActionResult GetPdf(int userId)
        {

            var pdfBytes = GeneratePdfReport(11);

            return File(pdfBytes, "application/pdf", "relatorio.pdf");
        }

        [HttpGet]
        public byte[] GeneratePdfReport(int employeeId)
        {
            using (var originalStream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(originalStream);
                var pdf = new PdfDocument(pdfWriter);
                var document = new Document(pdf);

                // Adiciona informações sobre a empresa e o funcionário
                document.Add(new Paragraph("Relatório de Pagamento"));
                document.Add(new Paragraph("Empresa: Tz Soluções"));
                document.Add(new Paragraph($"Funcionário: {employeeId}"));

                // Adiciona uma tabela para os valores
                var table = new Table(4); 
                table.AddCell("Descrição");
                table.AddCell("Valor Somado");
                table.AddCell("Valor Subtraído");
                table.AddCell("Total");

                double sumTotal = 10;
                double subtractTotal = 4;

                AddLineToTable(table, "Salário", 1500, 0);
                AddLineToTable(table, "Impostos", 0, 100);
                AddLineToTable(table, "Desconto de Horas", 0, 10);
                AddLineToTable(table, "Totais", sumTotal, subtractTotal);

                document.Add(table);
                var newStream = new MemoryStream(originalStream.ToArray());
                newStream.Position = 0;
                originalStream.Close();

                return newStream.ToArray();
            }
        }

        private void AddLineToTable(Table table, string descricao, double valorSomado, double valorSubtraido)
        {
            table.AddCell(descricao);
            table.AddCell(valorSomado.ToString("C2")); // Formata como moeda
            table.AddCell(valorSubtraido.ToString("C2"));
            table.AddCell((valorSomado - valorSubtraido).ToString("C2"));
        }
    }
}
