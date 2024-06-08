
using FoodUserNotifier.Core.Interfaces.Repositories;

namespace FoodUserNotifier.Core.Interfaces;

public interface IUnitOfWork
{ 
    IReportsRepository GetReportsRepository();
    ITelegramSessionsRepository GetTelegramSessionsRepository();
    Task SaveChangesAsync();
}
