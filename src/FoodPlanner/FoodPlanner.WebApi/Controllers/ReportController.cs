using FoodManager.Shared.Types;
using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.WebApi.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }
             
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
            _logger.LogInformation("Report {ReportGuid} generated successfully", report.Id);

            return Ok(report);
        }
    }
}
