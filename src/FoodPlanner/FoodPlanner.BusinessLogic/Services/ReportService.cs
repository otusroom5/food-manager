using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Reports;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Interfaces;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportService : IReportService
{   
    private readonly IStorageRepository _storageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
        _storageRepository = _unitOfWork.GetStorageRepository();
    }

    public Report Create(ReportType reportType, string reportName, string reportDescription, Guid userId)
    {        
        return Report.CreateNew(ReportId.CreateNew(), ReportName.FromString(reportName), reportType, reportDescription, UserId.FromGuid(userId));
    }

    public byte[] Generate(ReportType reportType)
    {
        byte[] reportContent = reportType switch
        {
            ReportType.ExpiredProducts => new ExpiredProductsReport(_storageRepository).Prepare(),
            ReportType.ConsumptionProducts => throw new NotImplementedException(),
            ReportType.PurchasingProducts => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
        return reportContent;
    }
}
