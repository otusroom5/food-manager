namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportFileBuilder
{
    IReportFileBuilder BuildHeader();
    IReportFileBuilder BuildBody(int daysBeforeExpired);
    IReportFileBuilder AddActualFoodPrices();
    IReportFileBuilder BuildFooter();
    string Build();
}
