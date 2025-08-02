using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Forms;

namespace InkFlow
{
    internal class Saver
    {
        public static void SaveAsDocx(string path, string text)
        {
            var doc = DocX.Create(path);
            doc.InsertParagraph(text);
            doc.Save();
        }

        public static void SaveAsPdf(string path, string text)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Exported Text";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Segoe UI", 12, XFontStyleEx.Regular);

            double margin = 40;
            double lineHeight = font.GetHeight();
            double y = margin;
            double maxY = page.Height - margin;

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (y + lineHeight > maxY)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = margin;
                }

                gfx.DrawString(line, font, XBrushes.Black, new XRect(margin, y, page.Width - 2 * margin, lineHeight), XStringFormats.TopLeft);
                y += lineHeight;
            }

            document.Save(path);
            document.Close();
        }
    }
}
