using FoodPlanner.DataAccess.Entities;

namespace FoodPlanner.DataAccess.Interfaces;

public interface IReportRepository
{
    void Create(ReportEntity report);
    void Delete(Guid id);
    byte[]? GetAttachmentById(Guid id);
}
