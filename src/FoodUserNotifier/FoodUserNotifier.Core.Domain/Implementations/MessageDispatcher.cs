using FoodUserNotifier.Core.Domain.Interfaces;
using FoodUserNotifier.Core.Entities;
using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Sources;

namespace FoodUserNotifier.BusinessLogic.Services;

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IDomainLogger _logger;
    private readonly IMessageSenderCollection _senderCollection;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecepientsSource _recepientsSource;

    public MessageDispatcher(IUnitOfWork unitOfWork,
        IMessageSenderCollection senderCollection,
        IRecepientsSource recepientsSource,
        IDomainLogger logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _senderCollection = senderCollection;
        _recepientsSource = recepientsSource;
    }

    public async Task SendAllAsync(Notification notification)
    {
        if (notification == null) throw new ArgumentNullException(nameof(notification));

        IEnumerable<Recepient> recepients = await _recepientsSource.GetAllByRecepientGroup(notification.Group);

        foreach (IMessageSender sender in _senderCollection.GetMessageSenders())
        {
            var message = new Message()
            {
                Recepients = recepients,
                MessageText = notification.Message,
                AttachmentIds = notification.AttachmentIds,
            };

            var report = GenerateDeliveryReport(notification);

            try
            {
                await sender.SendAsync(message, report);
            }
            catch (Exception ex)
            {
                report.Message = ex.Message;
                report.Success = false;
                _logger.Error(ex.Message);
            }
            finally
            {
                _unitOfWork
                    .GetDeliveryReportsRepository()
                    .Create(report);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

    private DeliveryReport GenerateDeliveryReport(Notification notification)
    {
        return new DeliveryReport()
        {
            Id = Guid.NewGuid(),
            NotificationId = notification.Id,
            Message = "Success",
            Success = true
        };
    }
}
