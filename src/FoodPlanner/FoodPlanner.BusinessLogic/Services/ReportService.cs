using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
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

    public Report Create(ReportType reportType)
    {
        throw new NotImplementedException();
    }

    public byte[] Generate()
    {
        throw new NotImplementedException();
    }
}
