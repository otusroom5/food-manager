using FoodPlanner.BusinessLogic.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace FoodPlanner.BusinessLogic.Services;

public class PdfHandlerService: IPdfHandlerService
{       
    public byte[] CreateDocument(string text)
    {
        var document = new PdfSharp.Pdf.PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Times New Roman", 14);
        var tf = new XTextFormatter(gfx);

        var rect = new XRect(10, 10, 350, 220);
        gfx.DrawRectangle(XBrushes.White, rect);
        tf.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);

        byte[] fileContents;
        using (var stream = new MemoryStream())
        {
            document.Save(stream, true);
            fileContents = stream.ToArray();
        }

        return fileContents;
    }   
}
