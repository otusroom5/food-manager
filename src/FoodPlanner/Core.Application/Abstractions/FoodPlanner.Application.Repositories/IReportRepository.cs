using FoodPlanner.Domain.Entities.ReportEntity;

namespace FoodPlanner.Application.Repositories;

public interface IReportRepository
{
    public void Create(Report report);
    public Report FindById(ReportId reportId);
    public IEnumerable<Report> GetByReportType(ReportType reportType);
    public IEnumerable<Report> GetByReportState(ReportState reportState);
    public IEnumerable<Report> GetAll();
    public void Change(Report report);
    public void Delete(Report report);
}
