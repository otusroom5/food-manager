using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodUserNotifier.Infrastucture.Repositories;

public class DeliveryReportsRepository : IDeliveryReportsRepository
{
    private readonly DatabaseContext _context;

    public DeliveryReportsRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(DeliveryReport report)
    {
        _context.Add(report);
    }

    public async Task<DeliveryReport> FindByNotificationIdAsync(Guid notificationId)
    {
        return await _context.DeliveryReports.FirstOrDefaultAsync(r => r.NotificationId == notificationId);
    }

    public async Task<DeliveryReport> GetAsync(Guid id)
    {
        return await _context.DeliveryReports.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
