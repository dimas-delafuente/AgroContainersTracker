using AgroContainerTracker.Core.Reports;
using AgroContainerTracker.Domain.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class LabelReportBase : ReportBase
    {
        private const string BAR_CODE_URL = "https://barcode.tec-it.com/barcode.ashx?data={0}&code=Code128&dpi=96";

        private readonly HttpClient _httpClient;
        private readonly ILogger<ILabelReportService> _logger;

        public LabelReportBase(IOptions<ReportsConfig> reportsConfig, HttpClient httpClient, ILogger<ILabelReportService> logger) : base(reportsConfig)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected async Task<PdfPCell> GetReferenceBarCode(string referenceNumber, float width, float height)
        {
            PdfPCell imageCell = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(string.Format(BAR_CODE_URL, referenceNumber)).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    byte[] barCodeBytes = await response.Content.ReadAsByteArrayAsync();
                    Image barCode = Image.GetInstance(barCodeBytes);
                    barCode.ScaleToFit(width, height);

                    imageCell = new PdfPCell(barCode);
                    imageCell.DisableBorderSide(15);
                    imageCell.Padding = 5;
                    imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error generating reference bar code. Reference Number: {referenceNumber}", referenceNumber);
            }

            return imageCell;
        }

        protected PdfPCell NewProductCell(string text)
        {
            PdfPCell productCell = new PdfPCell(new Phrase(DEFAULT_LEADING, text, FontFactory.GetFont(BASE_FONT, 16, Font.NORMAL)));
            productCell.Padding = 5;
            productCell.EnableBorderSide(BOX_BORDER);
            productCell.HorizontalAlignment = Element.ALIGN_CENTER;
            productCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return productCell;
        }

        protected IEnumerable<PdfPCell> NewCellRow(params string[] list)
        {
            foreach (var cell in list)
            {
                yield return NewProductCell(cell);
            }
        }
    }
}
