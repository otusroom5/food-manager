using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Types;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportService
{
    public Report Create(string reportName, string reportDescription, Guid userId);
    public byte[] Generate();    
}
