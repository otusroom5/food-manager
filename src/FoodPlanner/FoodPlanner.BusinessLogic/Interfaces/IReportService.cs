using FoodPlanner.BusinessLogic.Models;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportService
{
    public void Create(Report report);
    public void Generate();    
}
