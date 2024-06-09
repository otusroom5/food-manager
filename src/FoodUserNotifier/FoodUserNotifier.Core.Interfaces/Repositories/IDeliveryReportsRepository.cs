using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Core.Interfaces.Repositories;

public interface IDeliveryReportsRepository
{
    void Create(DeliveryReport report);
    Task<DeliveryReport> GetAsync(Guid id);
    Task<DeliveryReport> FindByNotificationIdAsync(Guid notificationId);
}
