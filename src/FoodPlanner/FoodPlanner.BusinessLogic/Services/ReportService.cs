using FoodPlanner.BusinessLogic.Interfaces;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportService
{
    private readonly IReportService _reportService;
    public ReportService(IReportService reportService)
    {
        _reportService = reportService;           
    }
    
    public void Generate()
    {
        _reportService.Generate();
    }
}