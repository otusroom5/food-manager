using FoodPlanner.BusinessLogic.Converters;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Entities;
using FoodPlanner.MessageBroker;

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

    private void PrepareReport(string productsJson)
    {
        // Need implement try parse to detect what kind report do we need
        var products = JsonExpireProductConverter.Convert(productsJson);

        var report = _reportService.Create("ExpiryProducts",
            "Отчет о товарах с заканчивающимся сроком использования",
            Guid.NewGuid()
        );

        report.Content = _reportService.GenerateReportFileDistributionAsync(products).Result;
        report.State = ReportState.Generated;

        var attachment = new ReportEntity()
        {
            AttachmentId = Guid.NewGuid(),
            ReportContent = report.Content
        };
        _reportStorageSerivce.SaveInMemory(attachment);

        var messageDto = new MessageDto
        {
            Id = report.Id.ToGuid(),
            Group = "Manager",
            Message = "Report with expire products from scheduler"
        };
        messageDto.AttachmentIds.Add(attachment.AttachmentId);

        _rabbitMqProducer.SendReportMessage(messageDto);
        report.State = ReportState.Sent;       
    }
}
