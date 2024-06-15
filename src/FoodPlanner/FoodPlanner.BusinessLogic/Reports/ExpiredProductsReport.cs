using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.DataAccess.Interfaces;
using System.Text;

namespace FoodPlanner.BusinessLogic.Reports;

public class ExpiredProductsReport: IReport
{
    private readonly IPdfService _pdfService;
    private readonly IStorageRepository _storageRepository;
    public ExpiredProductsReport(IPdfService pdfService, IStorageRepository storageRepository) 
    {
        _pdfService = pdfService;
        _storageRepository = storageRepository;
    }

    public async Task<byte[]> PrepareAsync()
    {
        var htmlContent = await GetHtmlAsync();
        return await _pdfService.CreatePDFAsync(htmlContent);
    }   

    private Task<string> GetHtmlAsync()
    {
        var htmlContent = new StringBuilder();
        htmlContent.AppendLine("<div style = 'border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif;' >");
        htmlContent.AppendLine("<h1> Товары с закончившимся сроком использования </h1>");
        htmlContent.AppendLine("<table style = 'width: 100%; border-collapse: collapse;'>");
        htmlContent.AppendLine("<thead>");
        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' > Наименование товара </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' > Годен до </th>");
        htmlContent.AppendLine("</tr><hr/>");
        htmlContent.AppendLine("</thead>");
        htmlContent.AppendLine("<tbody>");

        foreach (var item in _storageRepository.GetExpiredProductsAsync().Result)
        {
            htmlContent.AppendLine("<tr>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.ProductId + " </td>");
            htmlContent.AppendLine("<td style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' >" + item.ExpiryDate + " </td>");
            htmlContent.AppendLine("</tr>");
        }
        htmlContent.AppendLine("</tbody>");
        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("</div>");

        return Task.FromResult(htmlContent.ToString());
    }
}
