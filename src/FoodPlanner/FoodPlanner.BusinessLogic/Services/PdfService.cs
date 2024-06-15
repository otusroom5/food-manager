using DinkToPdf;
using DinkToPdf.Contracts;
using FoodPlanner.BusinessLogic.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FoodPlanner.BusinessLogic.Services;

public class PdfService: IPdfService
{
    private readonly ILogger<PdfService> _logger;
    private readonly IConverter _converter;
    public PdfService(IConverter converter, ILogger<PdfService> logger)
    {
        _converter = converter;
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public Task<byte[]> CreatePDFAsync(string htmlContent)
    {
        try
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
        catch (Exception exception)
        {
            _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, "CreatePDFAsync", exception);
            throw;
        }
    }
}
