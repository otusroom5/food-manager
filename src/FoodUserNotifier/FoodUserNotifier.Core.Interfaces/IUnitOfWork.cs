
using FoodUserNotifier.Core.Interfaces.Repositories;

namespace FoodUserNotifier.Core.Interfaces;

public interface IUnitOfWork
{ 
    IDeliveryReportsRepository GetDeliveryReportsRepository();
    Task SaveChangesAsync();
}
