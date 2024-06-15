using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using FoodUserNotifier.Infrastructure.Repositories.Contract;
using System.ComponentModel.DataAnnotations;
using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Infrastucture.Repositories;

public class DeliveryReportsRepository : IDeliveryReportsRepository
{
    private readonly DatabaseContext _context;

    public DeliveryReportsRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void CreateDeliveryReport(DeliveryReport report)
    {
        _context.Add(report);
    }

    

    public async Task<DeliveryReport> FindByNotificationIdDeliveryReportAsync(Guid notificationId)
    { 
        DeliveryReport deliveryReport = new DeliveryReport();
        DeliveryReportDTO deliveryReportDTO = await _context.DeliveryReports.FirstOrDefaultAsync(r => r.NotificationId == notificationId);
        deliveryReport.Id = deliveryReportDTO.Id;
        deliveryReport.NotificationId = deliveryReportDTO.NotificationId;
        deliveryReport.Message = deliveryReportDTO.Message;
        deliveryReport.Success = deliveryReportDTO.Success;

        return deliveryReport;
    }

    

    public async Task<DeliveryReport> GetAsyncDeliveryReport(Guid id)
    {
        DeliveryReport deliveryReport = new DeliveryReport();
        DeliveryReportDTO deliveryReportDTO = await _context.DeliveryReports.FirstOrDefaultAsync(r => r.Id == id);
        deliveryReport.Id = deliveryReportDTO.Id;
        deliveryReport.NotificationId = deliveryReportDTO.NotificationId;
        deliveryReport.Message = deliveryReportDTO.Message;
        deliveryReport.Success = deliveryReportDTO.Success;

        return deliveryReport;

    }
}
