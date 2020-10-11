using AgroContainerTracker.Domain.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Options;
using System.IO;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class ReportBase : PdfPageEventHelper
    {
        private const float DEFAULT_MULTIPLIED_LEADING = 0;
        private readonly BaseFont pageNumberFormat = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        private readonly BaseFont footerFont;

        protected const int TABLE_WIDTH = 100;
        protected const int BOX_BORDER = 15;
        protected const float DEFAULT_LEADING = 15;
        protected const string DATE_FORMAT = "dd/MM/yyyy";
        protected const int FONT_UNDERLINE_BOLD = 5;
        protected const string BASE_FONT = "Base Font";
        protected const string SECONDARY_FONT = "Secondary Font";

        protected readonly BaseColor reportTitleColor = new BaseColor(70, 124, 194);
        protected readonly BaseColor reportBaseColor = new BaseColor(208, 221, 240);
        protected readonly Font companyFont;
        protected readonly Font companyDetailsFont;
        protected readonly Font companyDetailsFontUnderlined;
        protected readonly ReportsConfig reportsConfig;

        public ReportBase(IOptions<ReportsConfig> reportsConfig)
        {
            this.reportsConfig = reportsConfig.Value;
            try
            {
                FontFactory.Register(Path.Combine(Directory.GetCurrentDirectory(), this.reportsConfig.ReportSecondaryFontPath), SECONDARY_FONT);
                FontFactory.Register(Path.Combine(Directory.GetCurrentDirectory(), this.reportsConfig.ReportBaseFontPath), BASE_FONT);
                footerFont = BaseFont.CreateFont(Path.Combine(Directory.GetCurrentDirectory(), this.reportsConfig.ReportBaseFontPath), BaseFont.CP1252, false);

                this.companyFont = FontFactory.GetFont(SECONDARY_FONT, 15, Font.BOLD);
                this.companyDetailsFont = FontFactory.GetFont(BASE_FONT, 10, Font.NORMAL);
                this.companyDetailsFontUnderlined = FontFactory.GetFont(BASE_FONT, 9, FONT_UNDERLINE_BOLD);
            }
            catch
            {
                FontFactory.Register(BaseFont.HELVETICA, SECONDARY_FONT);
                FontFactory.Register("Calibri", BASE_FONT);
                this.footerFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            }
        }

        protected PdfPCell WorkplaceCell()
        {
            PdfPCell workplaceCell = new PdfPCell();
            workplaceCell.Border = 0;
            workplaceCell.AddElement(Paragraph(reportsConfig.CompanyName, companyFont));
            workplaceCell.AddElement(new Paragraph());
            workplaceCell.AddElement(Paragraph("Centro de Trabajo", companyDetailsFontUnderlined, 20));
            workplaceCell.AddElement(Paragraph("Finca \"La Piñuela\" - Autovía Madrid-Lisboa, 354", companyDetailsFont, 12));
            workplaceCell.AddElement(Paragraph("06480    MÉRIDA", companyDetailsFont, 10));
            workplaceCell.AddElement(Paragraph("BADAJOZ", companyDetailsFont, 10));
            workplaceCell.AddElement(Paragraph("+34 606 562 657", companyDetailsFont, 10));

            return workplaceCell;
        }

        protected PdfPCell CompanyAddresCell()
        {
            PdfPCell companyAddressCell = new PdfPCell();
            companyAddressCell.Border = 0;
            companyAddressCell.AddElement(new Paragraph());
            companyAddressCell.AddElement(Paragraph("Domicilio Fiscal y Postal", companyDetailsFontUnderlined, 20));
            companyAddressCell.AddElement(Paragraph("C.I.F.  ES -  B06726616", companyDetailsFont, 14));
            companyAddressCell.AddElement(Paragraph("C/ LA CRUZ, 12 - 06480 MONTIJO (Badajoz)", companyDetailsFont, 10));
            companyAddressCell.AddElement(Paragraph("pinuelaagrofruit@gmail.com", companyDetailsFont, 10));

            return companyAddressCell;
        }

        protected Paragraph Paragraph(string text, Font font, float? fixedLeading = null, float? multipliedLeading = null)
        {
            var paragraph = new Paragraph(text, font);

            if (fixedLeading != null)
                paragraph.SetLeading((float)fixedLeading, multipliedLeading ?? DEFAULT_MULTIPLIED_LEADING);

            return paragraph;
        }

        protected void AddLogo(PdfWriter writer, float size, float x, float y)
        {
            PdfContentByte cb = writer.DirectContent;

            //Write company logo
            try
            {
                Image png = Image.GetInstance(Path.Combine(Directory.GetCurrentDirectory(), this.reportsConfig.CompanyLogoPath));
                png.ScaleToFit(size, size);
                png.SetAbsolutePosition(x, y);
                cb.AddImage(png);
            }
            catch
            {
                //Add company name
            }
        }

        protected void AddPageNumber(PdfWriter writer)
        {
            PdfContentByte cb = writer.DirectContent;
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);

            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();

            // Begin writing the footer
            tmpFooter.BeginText();

            WriteTemplateText(tmpFooter, reportsConfig.CompanyName, 10, footerFont, 0, 45);
            WriteTemplateText(tmpFooter, $"Página: {writer.PageNumber.ToString("00")}", 9, pageNumberFormat, 480, 45);

            // End text
            tmpFooter.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(580, 60);
            cb.Stroke();

            cb.AddTemplate(tmpFooter, 30, 0);
        }

        private void WriteText(PdfContentByte cb, string text, int size, BaseFont font, int x, int y)
        {
            cb.SetFontAndSize(font, size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
        }

        private void WriteTemplateText(PdfTemplate template, string text, int size, BaseFont font, int x, int y)
        {
            template.SetFontAndSize(font, size);
            template.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
        }

    }
}
