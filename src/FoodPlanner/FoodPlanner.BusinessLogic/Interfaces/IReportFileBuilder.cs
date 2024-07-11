using FoodPlanner.BusinessLogic.Models;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportFileBuilder
{
    IReportFileBuilder BuildHeader();
    IReportFileBuilder BuildBody(int daysBeforeExpired);
    IReportFileBuilder BuildBodyDistrubution(ExpireProduct products);
    IReportFileBuilder BuildBodyDistrubution(ProductAlmostOver product);
    IReportFileBuilder AddActualFoodPrices(int daysBeforeExpired);
    IReportFileBuilder BuildFooter();
    string Build();
}
