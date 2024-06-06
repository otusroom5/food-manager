using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Core.Interfaces.Repositories;

public interface IReportsRepository
{
    void Create(Report report);
    Task<Report> Get(Guid id);
}
