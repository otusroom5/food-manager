using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.BusinessLogic.Reports;
using FoodPlanner.BusinessLogic.Types;
using FoodPlanner.DataAccess.Interfaces;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportService : IReportService
{
    private readonly ReportType _reportType;
    private readonly IStorageRepository _storageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork, ReportType reportType)
    {
        _reportType = reportType;
        _unitOfWork = unitOfWork;
        _storageRepository = _unitOfWork.GetStorageRepository();
    }

    public Report Create(ReportType reportType, string reportName, string reportDescription, Guid userId)
    {        
        return Report.CreateNew(ReportId.CreateNew(), ReportName.FromString(reportName), reportType, reportDescription, UserId.FromGuid(userId));
    }

    public byte[] Generate()
    {
        byte[] reportContent = _reportType switch
        {
            ReportType.ExpiredProducts => new ExpiredProductsReport().Prepare(),
            ReportType.ConsumptionProducts => throw new NotImplementedException(),
            ReportType.PurchasingProducts => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
        return reportContent;
    }
}
