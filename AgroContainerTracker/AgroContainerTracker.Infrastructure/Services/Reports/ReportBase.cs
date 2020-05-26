using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AgroContainerTracker.Infrastructure.Services.Reports
{
    public class ReportBase : PdfPageEventHelper
    {
        private const int TOP_MARGIN = 800;
        private const int LEFT_MARGIN = 40;
        private const float DEFAULT_MULTIPLIED_LEADING = 0;

        private const int FONT_UNDERLINE_BOLD = 5;

        private readonly BaseFont pageNumberFormat = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        private readonly BaseFont footerFormat = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, false);
        private readonly BaseFont helveticaFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

        private readonly BaseFont champagneFont;

        public ReportBase()
        {
            try
            {
                FontFactory.Register(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Fonts", "Champagne_bold.ttf"), "Champagne_bold");
                FontFactory.Register(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Fonts", "Calibri Light.ttf"), "Calibri_light");
                FontFactory.Register(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Fonts", "Calibri Bold 2.ttf"), "Calibri_bold");

                champagneFont = BaseFont.CreateFont(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Fonts", "Champagne_bold.ttf"), BaseFont.CP1252, false);
            }
            catch (Exception exception)
            {

            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            this.AddPageHeader(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            this.AddPageNumber(writer, document);
        }


        private void AddPageHeader(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            Font companyFont = FontFactory.GetFont("Champagne_bold", 14, Font.BOLD);
            Font companyDetailsFont = FontFactory.GetFont("Calibri_light", 9, Font.NORMAL);
            Font companyDetailsFontUnderlined = FontFactory.GetFont("Calibri_bold", 8, FONT_UNDERLINE_BOLD);

            //Write company logo
            try
            {
                Image png = Image.GetInstance(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images", "PAgrofruit_original.jpg"));
                // Add a logo to the invoice
                png.ScaleToFit(200, 200);
                png.SetAbsolutePosition(350, 750);
                cb.AddImage(png);
            }
            catch (Exception exception)
            {
                //Add company name
            }


            // Add Header for Company information
            document.Add(Paragraph("PIÑUELA AGROFRUIT S.L.", companyFont));
            document.Add(new Paragraph());
            document.Add(Paragraph("Centro de Trabajo", companyDetailsFontUnderlined, 20));
            document.Add(Paragraph("Finca \"La Piñuela\" - Autovía Madrid-Lisboa, 354", companyDetailsFont, 12));
            document.Add(Paragraph("06480    MÉRIDA", companyDetailsFont, 10));
            document.Add(Paragraph("BADAJOZ", companyDetailsFont, 10));
            document.Add(Paragraph("+34 606 562 657", companyDetailsFont, 10));

            document.Add(new Paragraph());
            document.Add(Paragraph("Domicilio Fiscal y Postal", companyDetailsFontUnderlined, 20));
            document.Add(Paragraph("C.I.F.  ES -  B06726616", companyDetailsFont, 12));
            document.Add(Paragraph("C/ LA CRUZ, 12 - 06480 MONTIJO (Badajoz)", companyDetailsFont, 10));
            document.Add(Paragraph("pinuelaagrofruit@gmail.com", companyDetailsFont, 10));

        }

        private void AddPageNumber(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);

            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();

            // Begin writing the footer
            tmpFooter.BeginText();

            WriteTemplateText(tmpFooter, "PIÑUELA AGROFRUIT S.L.", 10, champagneFont, 0, 45);
            WriteTemplateText(tmpFooter, $"Página nº : {writer.PageNumber.ToString("00")}", 9, pageNumberFormat, 480, 45);

            // End text
            tmpFooter.EndText();

            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(570, 60);
            cb.Stroke();

            cb.AddTemplate(tmpFooter, 30, 1);
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

        private Paragraph Paragraph(string text, Font font, float? fixedLeading = null, float? multipliedLeading = null)
        {
            var paragraph = new Paragraph(text, font);

            if (fixedLeading != null)
                paragraph.SetLeading((float) fixedLeading, multipliedLeading ?? DEFAULT_MULTIPLIED_LEADING);

            return paragraph;
        }
    }
}
