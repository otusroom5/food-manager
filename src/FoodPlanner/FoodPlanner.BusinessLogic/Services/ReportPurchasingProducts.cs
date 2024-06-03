using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportPurchasingProducts : IReportService
{
    private readonly IStorageServiceClient _storageServiceClient;  

    public ReportPurchasingProducts(IStorageServiceClient storageServiceClient)
    {
        _storageServiceClient = storageServiceClient;  
    }

    public void Create(Report report)
    {
        throw new NotImplementedException();
    }

    public void Generate()
    {
        throw new NotImplementedException();
    }
}
