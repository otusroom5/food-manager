using Microsoft.AspNetCore.Mvc;

namespace FoodUserNotifier.Application.WebAPI.Interfaces
{
    public interface IDeliveryReportsController
    {
        public Task<IActionResult> GetByNotificationId(Guid notificationId);
    }
}
