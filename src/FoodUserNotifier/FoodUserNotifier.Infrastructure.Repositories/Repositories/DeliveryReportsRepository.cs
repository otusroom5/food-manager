using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using FoodUserNotifier.Infrastructure.Repositories.Contract;
using System.ComponentModel.DataAnnotations;
using FoodUserNotifier.Core.Entities.Types;

namespace FoodUserNotifier.Infrastucture.Repositories;

internal class DeliveryReportsRepository : IDeliveryReportsRepository, IRecepientRepository
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



    public IEnumerable<Recepient> GetAllForRoleRecepient(Role role)
    {
        throw new NotImplementedException();
    }

    public void CreateRecepient(Recepient recepient)
    {
        RecepientDTO recepientDTO = new RecepientDTO();

        if (recepient.ContactType == ContactType.Email) { recepientDTO.Email = recepient.Address; } else { recepientDTO.Telegram = recepient.Address; }
        recepientDTO.RecepientId = recepient.RecepientId;
        _context.Recipient.Add(recepientDTO);
        _context.SaveChanges();
    }

    public void DeleteRecepient(Guid id)
    {
        _context.Recipient.RemoveRange(_context.Recipient.Where(recepient => recepient.Id == id));
        _context.SaveChanges();
    }



    public async Task<Recepient> GetRecepientById(Guid id)
    {
        RecepientDTO recepientDTO = await _context.Recipient.FindAsync(id);
        Recepient recepient = new Recepient();

        if (recepientDTO.Email != null) { recepient.Address = recepientDTO.Email; recepient.ContactType = ContactType.Email; }
        else { recepient.Address = recepientDTO.Telegram; recepient.ContactType = ContactType.Telegram; }
        recepient.Id = recepientDTO.Id;
        recepient.RecepientId = recepientDTO.RecepientId;

        return recepient;

    }

    public void UpdateRecepient(Recepient recepient)
    {
        RecepientDTO recepientDTO =   _context.Recipient.Single(r => r.Id== recepient.Id);

        if(recepient.ContactType==ContactType.Email) { recepientDTO.Email = recepient.Address; }
        if (recepient.ContactType == ContactType.Telegram) { recepientDTO.Telegram = recepient.Address; }
        recepientDTO.RecepientId= recepient.RecepientId;
      
        _context.SaveChanges();
    }
}
