using FoodPlanner.BusinessLogic.Models;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportService
{
    public Report Create(string reportName, string reportDescription, Guid userId);
    Task<byte[]> GenerateReportFileAsync(int daysBeforeExpired);
    Task<byte[]> PreparePdfAsync(string html);
}
