using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Types;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportService
{
    public Report Create(ReportType reportType);
    public byte[] Generate();    
}
