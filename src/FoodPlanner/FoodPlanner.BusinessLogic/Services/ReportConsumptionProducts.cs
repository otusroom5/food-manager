using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.BusinessLogic.Models;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportConsumptionProducts : IReportService
{
    private readonly IStorageServiceClient _storageServiceClient;  

    public ReportConsumptionProducts(IStorageServiceClient storageServiceClient)
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
