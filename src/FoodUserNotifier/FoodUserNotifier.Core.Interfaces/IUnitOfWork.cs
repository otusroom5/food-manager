
using FoodUserNotifier.Core.Interfaces.Repositories;

namespace FoodUserNotifier.Core.Interfaces;

public interface IUnitOfWork
{ 
    IDeliveryReportsRepository GetDeliveryReportsRepository();
    ITelegramSessionsRepository GetTelegramSessionsRepository();
    Task SaveChangesAsync();
}
