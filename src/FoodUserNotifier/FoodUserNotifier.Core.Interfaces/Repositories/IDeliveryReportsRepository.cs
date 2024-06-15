using FoodUserNotifier.Core.Entities;

namespace FoodUserNotifier.Core.Interfaces.Repositories;

public interface IDeliveryReportsRepository
{
    void CreateDeliveryReport (DeliveryReport report);
    Task<DeliveryReport> GetAsyncDeliveryReport (Guid id);
    Task<DeliveryReport> FindByNotificationIdDeliveryReportAsync(Guid notificationId);

}
