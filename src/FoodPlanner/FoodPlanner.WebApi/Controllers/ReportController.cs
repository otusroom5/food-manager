﻿using FoodManager.Shared.Types;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.MessageBroker;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer, ApiKey", Roles = UserRole.Manager)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReportController : Abstractions.ControllerBase
    {       
        private readonly IReportService _reportService;
        private readonly IReportStorageSerivce _reportStorageSerivce;
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IHttpClientFactory httpClientFactory,          
            IReportService reportService,
            IReportStorageSerivce reportStorageSerivce,
            IRabbitMqProducer rabbitMqProducer,
            ILogger<ReportController> logger) : base(httpClientFactory)
        {                     
            _reportService = reportService;
            _reportStorageSerivce = reportStorageSerivce;
            _rabbitMqProducer = rabbitMqProducer;   
            _logger = logger;
        }

        [HttpGet("GenerateExpireProductsReport")]
        public ActionResult GenerateExpireProductsReport(int daysBeforeExpired = 0, bool includeActualPrices = false)
        {           
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("GenerateExpireProductsReport is not valid");
                BadRequest();
            }
            
            var report = _reportService.Create("ExpiryProducts",
                "Отчет о товарах с заканчивающимся сроком использования",
                Guid.NewGuid()
            ); 
            var reportId = report.Id.ToGuid();
            _logger.LogInformation("Report created: {ReportGuid}", reportId);

            report.Content = _reportService.GenerateReportFileAsync(daysBeforeExpired, includeActualPrices).Result;
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
                Message = "Report with expire products"
            };
            messageDto.AttachmentIds.Add(attachment.AttachmentId);

            _rabbitMqProducer.SendReportMessage(messageDto);
             report.State = ReportState.Sent;
            _logger.LogInformation("Report {ReportGuid} published to gueue successfully", reportId);

            return Ok(report.Id);
        }

        [HttpGet("GetReportAttachment")]
        public FileResult GetReportAttachment(Guid attachmentId)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("GetReportAttachments is not valid");
                BadRequest();
            }

            var attachment = _reportStorageSerivce.GetFromMemory(attachmentId);
            if (attachment != null)
            {
                Stream stream = new MemoryStream(attachment) { Position = 0 };
        
                return File(stream,
                            "application/pdf",
                            $"report_{DateTime.Now}.pdf");
            }
            else
            {
                throw new ArgumentNullException($"Can not find report attachment: {attachmentId}");
            }
        }    
    }
}
