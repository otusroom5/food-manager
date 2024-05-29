using FoodPlanner.Domain.Entities.ReportEntity;

namespace FoodPlanner.Application.Services;

public interface IReportService
{
    public Guid Create(Report report);
    public Report GetById(ReportId reportId);  
    public void SendReport(string reportId);
    public void Delete(string reportId);
}
