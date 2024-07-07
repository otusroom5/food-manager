using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.DataAccess.Interfaces;
using System.Text;

namespace FoodPlanner.BusinessLogic.Reports;

public class ReportFileBuilder : IReportFileBuilder
{
    private readonly ReportFile _reportFile;
    private readonly IStorageRepository _storageRepository;

    public ReportFileBuilder(IUnitOfWork unitOfWork)
    {
        _storageRepository = unitOfWork.GetStorageRepository();
        _reportFile = new ReportFile();
    }

    public IReportFileBuilder BuildHeader()
    {
        var htmlContent = new StringBuilder();
        htmlContent.AppendLine("<head><meta charset=utf-8></head>");
        htmlContent.AppendLine("<body>");
        htmlContent.AppendLine("<div style = 'border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif;' >");
        htmlContent.AppendLine("<h1> Товары с заканчивающимся сроком использования </h1>");
        htmlContent.AppendLine("<table style = 'width: 100%; border-collapse: collapse;'>");
        htmlContent.AppendLine("<thead>");
        htmlContent.AppendLine("<tr>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' > Наименование товара </th>");
        htmlContent.AppendLine("<th style = 'padding: 8px; text-align: left; border-bottom: 1px solid #ddd;' > Годен до </th>");
        htmlContent.AppendLine("</tr><hr/>");
        htmlContent.AppendLine("</thead>");
        htmlContent.AppendLine("<tbody>");

        _reportFile.Header = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder BuildBody()
    {
        var htmlContent = new StringBuilder();
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
        htmlContent.AppendLine("</body>");

        _reportFile.Body = htmlContent.ToString();
        return this;
    }

    public IReportFileBuilder AddActualFoodPrices()
    {
       
        return this;
    }

    public IReportFileBuilder BuildFooter()
    {
        _reportFile.Footer += $"&copy; {DateTime.Now.Date.Year} - FoodManager";
        return this;
    }

    public string Build()
    {
        return _reportFile.ToString();
    }
}