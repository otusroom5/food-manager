using DinkToPdf;
using DinkToPdf.Contracts;
using FoodPlanner.BusinessLogic.Interfaces;

namespace FoodPlanner.BusinessLogic.Services;

public class PdfService: IPdfService
{
    private readonly IConverter _converter;
    public PdfService(IConverter converter)
    {
        _converter = converter;
    }

    public Task<byte[]> CreatePDFAsync(string htmlContent)
    {
        var globalSettings = new GlobalSettings()
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 20, Left = 30, Right = 30, Bottom = 20 }
        };

        var objectSettings = new ObjectSettings()
        {
            PagesCount = true,
            HtmlContent = htmlContent           
        };

        var document = new HtmlToPdfDocument()
        {
            GlobalSettings = globalSettings,
            Objects = { objectSettings }
        };

        return Task.FromResult(_converter.Convert(document));
    }
}
