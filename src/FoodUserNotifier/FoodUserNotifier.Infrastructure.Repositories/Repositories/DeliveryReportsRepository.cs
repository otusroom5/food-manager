using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.Infrastucture.Repositories;

internal class DeliveryReportsRepository : IDeliveryReportsRepository
{
    private readonly DatabaseContext _databaseContext;

    public DeliveryReportsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Create(DeliveryReport report)
    {
        _databaseContext.Add(report);
    }

    public async Task<DeliveryReport> FindByNotificationIdAsync(Guid notificationId)
    {
        return await _databaseContext.DeliveryReports.FirstOrDefaultAsync(r => r.NotificationId == notificationId);
    }

    public async Task<DeliveryReport> GetAsync(Guid id)
    {
        return await _databaseContext.DeliveryReports.FirstOrDefaultAsync(r => r.Id == id);
    }
}
