using FoodPlanner.BusinessLogic.Models;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportService
{
    public Report Create(string reportName, string reportDescription, Guid userId);
    public byte[] GenerateReportFile();
    Task<byte[]> PreparePdfAsync(string html);
}
