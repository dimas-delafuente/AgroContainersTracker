using System;
using System.IO;
using AgroContainerTracker.Core.Services.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class PackagingMovementsReportService : ReportBase, IReportService
    {
        private const int MAX_COLUMN = 18;

        private PdfWriter pdfWriter;
        private Document document;
        private PdfPTable pdfPTable;
        private MemoryStream memoryStream;

        public PackagingMovementsReportService()
        {
            this.pdfPTable = new PdfPTable(MAX_COLUMN);
        }


        public byte[] BuildReport()
        {
            using (memoryStream = new MemoryStream())
            {
                document = new Document(PageSize.A4, 25, 25, 30, 1);
                pdfPTable.WidthPercentage = 90;
                pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                pdfWriter.PageEvent = new ReportBase();

                document.Open();

                this.ReportBody();

                //document.Add(pdfPTable);
                document.Close();

                return memoryStream.ToArray();
            }

        }

        private void ReportBody()
        {
            Font fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            Font _fontStyle = FontFactory.GetFont("Tahoma", 7f, 0);

            float[] sizes = new float[MAX_COLUMN];
            sizes[0] = 50;
            for (int i = 1; i < MAX_COLUMN; i++)
                sizes[i] = 100;

            pdfPTable.SetWidths(sizes);

            #region Basic info (1st Row)

            var pdfPCell = new PdfPCell(new Phrase("Empresa: ", fontStyle));
            pdfPCell.Colspan = 4;
            pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("SAT FRUTAS NENE E HIJOS", fontStyle));
            pdfPCell.Colspan = 5;
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);


            pdfPCell = new PdfPCell(new Phrase("Proveedor: ", fontStyle));
            pdfPCell.Colspan = 4;
            pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Ejemplo", fontStyle));
            pdfPCell.Colspan = 5;
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPTable.CompleteRow();

            #endregion

            #region Basic info (2st Row)

            pdfPCell = new PdfPCell(new Phrase("Fecha: ", fontStyle));
            pdfPCell.Colspan = 4;
            pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), fontStyle));
            pdfPCell.Colspan = 5;
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Cif: ", fontStyle));
            pdfPCell.Colspan = 4;
            pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("sfswefwef", fontStyle));
            pdfPCell.Colspan = 5;
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.ExtraParagraphSpace = 0;
            pdfPCell.Border = 0;
            pdfPTable.AddCell(pdfPCell);

            pdfPTable.CompleteRow();

            #endregion
        }
    }
}
