using FoodPlanner.BusinessLogic.Models;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportFileBuilder
{
    IReportFileBuilder BuildHeader();
    IReportFileBuilder BuildBody(int daysBeforeExpired);
    IReportFileBuilder BuildBodyDistrubution(ExpireProduct products);
    IReportFileBuilder AddActualFoodPrices();
    IReportFileBuilder BuildFooter();
    string Build();
}
