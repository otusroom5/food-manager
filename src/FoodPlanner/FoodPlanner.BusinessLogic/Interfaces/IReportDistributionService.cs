namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportDistributionService
{
    public Task DistributeAsync(string productsJson);
}