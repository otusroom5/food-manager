namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportFileBuilder
{
    IReportFileBuilder BuildHeader();
    IReportFileBuilder BuildBody();
    IReportFileBuilder AddActualFoodPrices();
    IReportFileBuilder BuildFooter();
    string Build();
}
