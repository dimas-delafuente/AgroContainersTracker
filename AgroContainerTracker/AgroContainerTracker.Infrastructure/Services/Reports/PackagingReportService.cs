using AgroContainerTracker.Core.Services.Reports;
using AgroContainerTracker.Domain.Companies;
using AgroContainerTracker.Domain.Packagings;
using AgroContainerTracker.Domain.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class PackagingReportService : ReportBase, IReportService<PackagingReport>
    {
        private readonly string[] headers = new string[] { "Envase", "Tipo", "Material", "Peso", "Color", "Fecha", "Client", "Cantidad", "Total" };
        private const int MAX_COLUMN = 9;
        private readonly float[] tableSizes = new float[MAX_COLUMN] { 70, 70, 80, 50, 70, 100, 250, 70, 70 };

        private PdfWriter pdfWriter;
        private Document document;
        private PackagingReport pkgReportData;
        public PackagingReportService(IOptions<ReportsConfig> reportsConfig) : base(reportsConfig)
        {

        }


        public async Task<byte[]> BuildReport(PackagingReport reportData)
        {
            this.pkgReportData = reportData;

            using (var memoryStream = new MemoryStream())
            {
                document = new Document(PageSize.A4, 25, 25, 30, 60);

                pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                pdfWriter.PageEvent = this;

                document.Open();

                await Task.Run(() => ReportBody(document, pkgReportData));

                document.Close();

                return memoryStream.ToArray();
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            AddPageHeader(writer, document, pkgReportData.Customer);
            document.Add(new Paragraph());
            AddReportTitle(document, pkgReportData.ReportTitle);
            AddTableHeader(document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            this.AddPageNumber(writer);
        }

        private void AddPageHeader(PdfWriter writer, Document document, Customer customer)
        {

            AddLogo(writer, 200, 350, 750);

            // Add Header for Company information
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = TABLE_WIDTH;
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell workplaceCell = WorkplaceCell();
            table.AddCell(workplaceCell);
            table.CompleteRow();

            PdfPCell companyAddressCell = CompanyAddresCell();
            table.AddCell(companyAddressCell);

            PdfPCell customerDetailsCell = new PdfPCell();
            customerDetailsCell.UseBorderPadding = true;
            customerDetailsCell.BackgroundColor = reportBaseColor;
            customerDetailsCell.PaddingLeft = 10;
            customerDetailsCell.PaddingBottom = 10;

            Font customerFont = FontFactory.GetFont(SECONDARY_FONT, 12, Font.BOLD);
            customerDetailsCell.AddElement(Paragraph(customer.Name, customerFont));
            customerDetailsCell.AddElement(Paragraph($"C.I.F. {customer.CompanyCode}", companyDetailsFont, 14));
            customerDetailsCell.AddElement(Paragraph(customer.Address, companyDetailsFont, 10));
            customerDetailsCell.AddElement(Paragraph($"{customer.PostalCode}   {customer.Locality}", companyDetailsFont, 10));
            customerDetailsCell.AddElement(Paragraph(customer.State, companyDetailsFont, 10));

            table.AddCell(customerDetailsCell);
            table.CompleteRow();

            document.Add(table);
        }

        private void AddReportTitle(Document document, string title)
        {
            Font titleFont = FontFactory.GetFont(BASE_FONT, 20, Font.BOLD, reportTitleColor);
            document.Add(Paragraph(title, titleFont, 40));
        }

        private void ReportBody(Document document, PackagingReport reportData)
        {
            var pdfPTable = new PdfPTable(MAX_COLUMN);
            pdfPTable.WidthPercentage = 100;
            pdfPTable.SetWidths(tableSizes);

            #region Table Body
            Font tableRowFont = FontFactory.GetFont(BASE_FONT, 10, Font.NORMAL);
            PdfPCell pdfPCell;

            foreach (var p in reportData.PackagingMovements)
            {
                pdfPCell = new PdfPCell(new Phrase(p.Packaging.Code, tableRowFont));
                pdfPCell.Padding = 5;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Packaging.Type.ToString(), tableRowFont));
                pdfPCell.Padding = 5;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Packaging.Material.ToString(), tableRowFont));
                pdfPCell.Padding = 5;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Packaging.Weight.ToString(), tableRowFont));
                pdfPCell.Padding = 5;
                pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Packaging.Color, tableRowFont));
                pdfPCell.Padding = 5;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Created.ToString(DATE_FORMAT), tableRowFont));
                pdfPCell.Padding = 5;
                pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Customer.Name, tableRowFont));
                pdfPCell.Padding = 5;
                pdfPTable.AddCell(pdfPCell);

                string operation = p.Operation.Equals(Operation.Add) ? "+" : "-";
                pdfPCell = new PdfPCell(new Phrase($"{operation}{p.Amount.ToString()}", tableRowFont));
                pdfPCell.Padding = 5;
                pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfPTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(p.Total.ToString(), tableRowFont));
                pdfPCell.Padding = 5;
                pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfPTable.AddCell(pdfPCell);

                pdfPTable.CompleteRow();
            }

            document.Add(pdfPTable);

            #endregion
        }

        private void AddTableHeader(Document document)
        {
            var pdfPTableHeader = new PdfPTable(MAX_COLUMN);
            pdfPTableHeader.WidthPercentage = 100;
            pdfPTableHeader.SpacingBefore = 10;
            pdfPTableHeader.SetWidths(tableSizes);


            foreach (PdfPCell headerCell in GetHeaderCells(headers))
            {
                pdfPTableHeader.AddCell(headerCell);
            }

            pdfPTableHeader.CompleteRow();
            document.Add(pdfPTableHeader);

        }

        private IEnumerable<PdfPCell> GetHeaderCells(params string[] headers)
        {
            Font tableHeaderFont = FontFactory.GetFont(BASE_FONT, 10, Font.BOLD);

            foreach (string header in headers)
            {
                var pdfPCell = new PdfPCell(new Phrase(header, tableHeaderFont));
                pdfPCell.Padding = 5;
                pdfPCell.BackgroundColor = reportBaseColor;
                yield return pdfPCell;
            }
        }

    }
}
