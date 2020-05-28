using System;
using System.IO;
using AgroContainerTracker.Domain.Reports;
using AgroContainerTracker.Core.Services.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Options;
using AgroContainerTracker.Domain.Packagings;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class PackagingReportService : ReportBase, IReportService<PackagingReport>
    {
        private const int MAX_COLUMN = 9;
        private readonly float[] tableSizes = new float[MAX_COLUMN] { 70, 70, 80, 50, 70, 100, 250, 70, 70 };

        private PdfWriter pdfWriter;
        private Document document;
        private PackagingReport pkgReportData;
        public PackagingReportService(IOptions<ReportsConfig> reportsConfig) : base(reportsConfig)
        {

        }


        public byte[] BuildReport(PackagingReport reportData)
        {
            this.pkgReportData = reportData;

            using (var memoryStream = new MemoryStream())
            {
                document = new Document(PageSize.A4, 25, 25, 30, 60);

                pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                pdfWriter.PageEvent = this;

                document.Open();

                ReportBody(document, pkgReportData);

                document.Close();

                return memoryStream.ToArray();
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.AddPageHeader(writer, document, pkgReportData.Customer);
            document.Add(new Paragraph());
            AddReportTitle(document, pkgReportData.ReportTitle);
            AddTableHeader(document);
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

            #region Table Header
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

            #region Table Header
            Font tableHeaderFont = FontFactory.GetFont(BASE_FONT, 10, Font.BOLD);

            var pdfPCell = new PdfPCell(new Phrase("Envase", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Tipo", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);


            pdfPCell = new PdfPCell(new Phrase("Material", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Peso", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Color", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Fecha", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Cliente", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Cantidad", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Total", tableHeaderFont));
            pdfPCell.Padding = 5;
            pdfPCell.BackgroundColor = reportBaseColor;
            pdfPTableHeader.AddCell(pdfPCell);

            pdfPTableHeader.CompleteRow();

            #endregion

            document.Add(pdfPTableHeader);

        }

    }
}
