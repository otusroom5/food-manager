using FoodManager.Shared.Types;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Entities;
using FoodPlanner.MessageBroker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Mail;
using System.Text.Json;

namespace FoodPlanner.WebApi.Controllers
{
    // [Authorize(Roles = UserRole.Manager)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IReportStorageSerivce _reportStorageSerivce;
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService,
            IReportStorageSerivce reportStorageSerivce,
            IRabbitMqProducer rabbitMqProducer,
            ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _reportStorageSerivce = reportStorageSerivce;
            _rabbitMqProducer = rabbitMqProducer;
            _logger = logger;
        }

        [HttpGet("GenerateExpiredProductsReport")]
        public ActionResult<Guid> GenerateExpiredProductsReport()
        {
            var report = _reportService.Create(ReportType.ExpiredProducts,
                "ExpiredProducts",
                "Отчет о товарах с заканчивающимся сроком использования",
                    Guid.NewGuid()
                );
            _logger.LogInformation("Report created: {ReportGuid}", report.Id);

            report.Content = _reportService.Generate(report.Type);
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
                Message = "Report with expired products"
            };
            messageDto.AttachmentIds.Add(attachment.AttachmentId);

            _rabbitMqProducer.SendReportMessage(messageDto);

            _logger.LogInformation("Report {ReportGuid} published to gueue successfully", report.Id);

            return Ok(report.Id);
        }

        [HttpGet("GetReportAttachment")]
        public ActionResult<FileResult> GetReportAttachment(Guid attachmentId)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("GetReportAttachments is not valid");
                BadRequest();
            }

            var attachment = _reportStorageSerivce.GetFromMemory(attachmentId);
            if (attachment != null)
            {
                Stream stream = new MemoryStream(attachment);

                return File(stream,
                            "application/pdf",
                            $"report_{DateTime.Now}.pdf");
            }
            else
            {
                throw new Exception($"Can not find report attachment: {attachmentId}");
            }            
        }
    }
}
