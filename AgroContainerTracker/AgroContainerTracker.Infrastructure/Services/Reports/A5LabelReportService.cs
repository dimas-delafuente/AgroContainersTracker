using AgroContainerTracker.Core.Reports;
using AgroContainerTracker.Domain.Packagings;
using AgroContainerTracker.Domain.ProductManagement;
using AgroContainerTracker.Domain.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class A5LabelReportService : LabelReportBase, ILabelReportService
    {

        private PdfWriter pdfWriter;
        private Document document;
        private LabelReport labelReportData;

        public A5LabelReportService(IOptions<ReportsConfig> reportsConfig, HttpClient httpClient, ILogger<A5LabelReportService> logger) : base(reportsConfig, httpClient, logger)
        {

        }

        public async Task<byte[]> BuildReport(LabelReport reportData)
        {
            this.labelReportData = reportData;

            using (var memoryStream = new MemoryStream())
            {
                document = new Document(PageSize.A5.Rotate(), marginLeft: 25, marginRight: 25, marginTop: 75, marginBottom: 25);

                pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                pdfWriter.PageEvent = this;

                document.Open();

                await ReportBody(document, labelReportData).ConfigureAwait(false);

                document.Close();

                return memoryStream.ToArray();
            }
        }

        private async Task ReportBody(Document document, LabelReport reportData)
        {
            var products = reportData.ProductWeighing.ProductRecords.Where(x => !x.Packaging.Type.Equals(PackagingType.Caja));

            foreach(ProductRecord product in products)
            {
                AddLogo(pdfWriter, size: 150, 25, 360);
                AddEntryDetails(document, reportData.ProductWeighing);
                AddCustomerDetails(document, reportData.ProductWeighing);
                AddFruitDetails(document, reportData.ProductWeighing);
                AddProductDetails(document, reportData.ProductWeighing);
                await AddReference(document, product.ReferenceNumber);
            }
        }

        private void AddEntryDetails(Document document, ProductWeighing productWeighing)
        {
            PdfPTable table = new PdfPTable(new float[] { 20, 60, 20 });
            table.WidthPercentage = TABLE_WIDTH;

            PdfPCell entryCell = new PdfPCell(new Phrase(DEFAULT_LEADING, "Nº ENTRADA:", FontFactory.GetFont(BASE_FONT, 10, Font.NORMAL)));
            entryCell.Padding = 5;
            entryCell.EnableBorderSide(BOX_BORDER);
            entryCell.BorderWidthRight = 0;
            entryCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(entryCell);

            PdfPCell entryNumber = new PdfPCell(new Phrase(DEFAULT_LEADING, productWeighing.ProductEntryNumber.ToString("000"), FontFactory.GetFont(BASE_FONT, 24, Font.BOLD)));
            entryNumber.Padding = 5;
            entryNumber.EnableBorderSide(BOX_BORDER);
            entryNumber.BorderWidthRight = 0;
            entryNumber.BorderWidthLeft = 0;
            entryNumber.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(entryNumber);

            PdfPCell entryDate = new PdfPCell(new Phrase(DEFAULT_LEADING, productWeighing.ProductEntry.EntryDate.ToString("dd/MM"), FontFactory.GetFont(BASE_FONT, 24, Font.BOLD)));
            entryDate.Padding = 5;
            entryDate.EnableBorderSide(BOX_BORDER);
            entryDate.BorderWidthLeft = 0;
            entryDate.HorizontalAlignment = Element.ALIGN_RIGHT;
            entryDate.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(entryDate);

            table.CompleteRow();

            document.Add(table);
        }

        private void AddCustomerDetails(Document document, ProductWeighing productWeighing)
        {
            PdfPTable table = new PdfPTable(new float[] { 20, 80 });
            table.SpacingBefore = 10;
            table.WidthPercentage = TABLE_WIDTH;

            PdfPCell buyerCell = new PdfPCell(new Phrase(DEFAULT_LEADING, "COMPRADOR:", FontFactory.GetFont(BASE_FONT, 10, Font.NORMAL)));
            buyerCell.Padding = 5;
            buyerCell.EnableBorderSide(BOX_BORDER);
            buyerCell.BorderWidthRight = 0;
            buyerCell.BorderWidthBottom = 0;
            buyerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(buyerCell);

            PdfPCell buyerNameCell = new PdfPCell(new Phrase(DEFAULT_LEADING, productWeighing.Buyer.Name, FontFactory.GetFont(BASE_FONT, 20, Font.BOLD)));
            buyerNameCell.Padding = 5;
            buyerNameCell.EnableBorderSide(BOX_BORDER);
            buyerNameCell.BorderWidthLeft = 0;
            buyerNameCell.BorderWidthBottom = 0;
            buyerNameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(buyerNameCell);

            table.CompleteRow();

            PdfPCell sellerCell = new PdfPCell(new Phrase(DEFAULT_LEADING, "FRUTICULTOR:", FontFactory.GetFont(BASE_FONT, 10, Font.NORMAL)));
            sellerCell.Padding = 5;
            sellerCell.EnableBorderSide(BOX_BORDER);
            sellerCell.BorderWidthRight = 0;
            sellerCell.BorderWidthTop = 0;
            sellerCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(sellerCell);

            PdfPCell sellerNameCell = new PdfPCell(new Phrase(DEFAULT_LEADING, productWeighing.Seller.Name, FontFactory.GetFont(BASE_FONT, 20, Font.BOLD)));
            sellerNameCell.Padding = 5;
            sellerNameCell.EnableBorderSide(BOX_BORDER);
            sellerNameCell.BorderWidthLeft = 0;
            sellerNameCell.BorderWidthTop = 0;
            sellerNameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(sellerNameCell);

            table.CompleteRow();

            document.Add(table);
        }

        private void AddFruitDetails(Document document, ProductWeighing productWeighing)
        {
            PdfPTable table = new PdfPTable(new float[] { 100 });
            table.SpacingBefore = 10;
            table.WidthPercentage = TABLE_WIDTH;

            PdfPCell fruitCell = new PdfPCell(new Phrase(DEFAULT_LEADING, productWeighing.Fruit.Name, FontFactory.GetFont(BASE_FONT, 24, Font.BOLD)));
            fruitCell.Padding = 5;
            fruitCell.EnableBorderSide(BOX_BORDER);
            fruitCell.HorizontalAlignment = Element.ALIGN_CENTER;
            fruitCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            table.AddCell(fruitCell);
            table.CompleteRow();

            document.Add(table);
        }

        private void AddProductDetails(Document document, ProductWeighing productWeighing)
        {
            float columnWidth = 100 / 6;
            PdfPTable table = new PdfPTable(new float[] { columnWidth, columnWidth, columnWidth, columnWidth, columnWidth, columnWidth });
            table.SpacingBefore = 10;
            table.WidthPercentage = TABLE_WIDTH;

            foreach(PdfPCell cell in NewCellRow("BRUTO", "DESTARE", "PESO NETO", "PALETS", "PALOTS", "CAJAS"))
            {
                table.AddCell(cell);
            }

            string palets = productWeighing.ProductRecords.Where(x => x.Packaging.Type.Equals(PackagingType.Palet)).Sum(x => x.Quantity).ToString();
            string palots = productWeighing.ProductRecords.Where(x => x.Packaging.Type.Equals(PackagingType.Palot)).Sum(x => x.Quantity).ToString();
            string boxes = productWeighing.ProductRecords.Where(x => x.Packaging.Type.Equals(PackagingType.Caja)).Sum(x => x.Quantity).ToString();
            foreach (PdfPCell cell in NewCellRow(productWeighing.GrossWeight.ToString(), productWeighing.TareWeight.ToString(), productWeighing.NetWeight.ToString(),
                palets, palots, boxes))
            {
                table.AddCell(cell);
            }

            table.CompleteRow();

            document.Add(table);
        }

        

        private async Task AddReference(Document document, string referenceNumber)
        {
            PdfPCell barCodeCell = await GetReferenceBarCode(referenceNumber, 130, 70).ConfigureAwait(false);
            float[] tableWidths = barCodeCell != null ? new float[] { 50, 50 } : new float[] { 100 };

            PdfPTable table = new PdfPTable(tableWidths);
            table.SpacingBefore = 5;
            table.DefaultCell.DisableBorderSide(15);
            table.WidthPercentage = TABLE_WIDTH;

            PdfPCell referenceCell = new PdfPCell(new Phrase(DEFAULT_LEADING, "Nº de Referencia/Lote", FontFactory.GetFont(BASE_FONT, 14, Font.NORMAL)));
            referenceCell.Padding = 5;
            referenceCell.DisableBorderSide(15);
            referenceCell.HorizontalAlignment = Element.ALIGN_CENTER;
            referenceCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(referenceCell);
            table.CompleteRow();

            PdfPCell referenceNumberCell = new PdfPCell(new Phrase(0, referenceNumber, FontFactory.GetFont(BASE_FONT, 60, Font.BOLD)));
            referenceNumberCell.Padding = 0;
            referenceNumberCell.DisableBorderSide(15);
            referenceNumberCell.HorizontalAlignment = Element.ALIGN_CENTER;

            if (barCodeCell != null)
            {
                table.AddCell(barCodeCell);
            }

            table.AddCell(referenceNumberCell);
            table.CompleteRow();

            document.Add(table);
        }
    }
}