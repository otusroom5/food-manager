using FoodManager.Shared.Types;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.EventBusRabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.WebApi.Controllers
{
   // [Authorize(Roles = UserRole.Manager)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IRabbitMqProducer _rabbitMQProducer;  
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, 
            IRabbitMqProducer rabbitMQProducer,
            ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _rabbitMQProducer = rabbitMQProducer;
            _logger = logger;
        }

        [HttpGet("GenerateExpiredProductsReport")]
        public ActionResult<Report> GenerateExpiredProductsReport()
        {            
            var report = _reportService.Create(ReportType.ExpiredProducts,
                "ExpiredProducts",
                "Отчет о товарах с заканчивающимся сроком использования",
                    Guid.NewGuid()
                );
            _logger.LogInformation("Report created: {ReportGuid}", report.Id);

            report.Content = new MemoryStream(_reportService.Generate(report.Type));
            report.State = ReportState.Generated;

            _logger.LogInformation("Report {ReportGuid} generated successfully. And publishing to gueue", report.Id);

            _rabbitMQProducer.SendReportMessage($"Report {report.Id}");

            _logger.LogInformation("Report {ReportGuid} published to gueue successfully", report.Id);

            return Ok(report);        
        }
    }
}
