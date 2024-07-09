using FoodPlanner.BusinessLogic.Converters;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Entities;
using FoodPlanner.MessageBroker;
using System.Net.Mail;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportDistributionService: IReportDistributionService
{
    private readonly IReportService _reportService;
    private readonly IReportStorageSerivce _reportStorageSerivce;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public ReportDistributionService(IReportService reportService,
            IReportStorageSerivce reportStorageSerivce,
            IRabbitMqProducer rabbitMqProducer)
    {
        _reportService = reportService;
        _reportStorageSerivce = reportStorageSerivce;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task DistributeAsync(string productsJson)
    {
        await Task.Run(() => PrepareReport(productsJson));  
    }

    private void PrepareReport(string messageJson)
    {
        if (messageJson.TryParseJson(out ExpireProduct products))
        {
            var report = CreateReport("ExpiryProducts",
                "Отчет о товарах с заканчивающимся сроком использования");

            report.Content = _reportService.GenerateReportFileDistributionAsync(products).Result;
            report.State = ReportState.Generated;

            var attachmentId = SaveAttachments(report.Content);

            SendToMessageBroker(report.Id.ToGuid(),
                attachmentId,
                "Report with expire products from scheduler");
            
            report.State = ReportState.Sent;
        }     
    }

    private Report CreateReport(string reportName, string reportDescription)
    {
        return _reportService.Create(reportName,
                                     reportDescription,
                                     Guid.NewGuid());
    }

    private Guid SaveAttachments(byte[] content)
    {
        var attachment = new ReportEntity()
        {
            AttachmentId = Guid.NewGuid(),
            ReportContent = content
        };
        _reportStorageSerivce.SaveInMemory(attachment);

        return attachment.AttachmentId;
    }

    private void SendToMessageBroker(Guid reportId, Guid attachmentId, string messageText)
    {
        var messageDto = new MessageDto
        {           
            Id = reportId,
            Group = "Manager",
            Message = messageText
        };
        messageDto.AttachmentIds.Add(attachmentId);

        _rabbitMqProducer.SendReportMessage(messageDto);
    }
}
