using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Reports;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportService : IReportService
{
    private readonly IStorageRepository _storageRepository;   
    private readonly IPdfService _pdfService;
    private readonly ILogger<ReportService> _logger;

    public ReportService(IUnitOfWork unitOfWork,
         IPdfService pdfService,
         ILogger<ReportService> logger)
    {
        _pdfService = pdfService;   
        _storageRepository = unitOfWork.GetStorageRepository();
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public Report Create(ReportType reportType, string reportName, string reportDescription, Guid userId)
    {
        try
        {
            return Report.CreateNew(ReportId.CreateNew(), ReportName.FromString(reportName), reportType, reportDescription, UserId.FromGuid(userId));
        }
        catch (Exception exception)
        {
            LogError("Create", exception);
            throw;
        }
    }

    public byte[] Generate(ReportType reportType)
    {
        try
        {
            byte[] reportContent = reportType switch
            {
                ReportType.ExpiredProducts => new ExpiredProductsReport(_pdfService, _storageRepository).PrepareAsync().Result,
                ReportType.ConsumptionProducts => throw new NotImplementedException(),
                ReportType.PurchasingProducts => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
            return reportContent;
        }
        catch (Exception exception)
        {
            LogError("Generate", exception);
            throw;
        }
    }    

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }
}
