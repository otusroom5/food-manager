using FoodPlanner.BusinessLogic.Interfaces;
using FoodPlanner.DataAccess.Entities;
using FoodPlanner.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace FoodPlanner.BusinessLogic.Services;

public class ReportStorageSerivce: IReportStorageSerivce
{
    private readonly IReportRepository _reportRepository;
    private readonly ILogger<ReportStorageSerivce> _logger;

    public ReportStorageSerivce(IUnitOfWork unitOfWork,     
         ILogger<ReportStorageSerivce> logger)
    {       
        _reportRepository = unitOfWork.GetReportRepository();
        _logger = logger;

        _logger.LogInformation("'{0}' handling.", GetType().Name);
    }

    public void SaveInMemory(ReportEntity reportEntity)
    {
        try
        {
            _reportRepository.Create(reportEntity);
        }
        catch (Exception exception)
        {
            LogError("SaveInMemory", exception);
            throw;
        }
    }

    public byte[]? GetFromMemory(Guid id) 
    {
        try
        {
            return _reportRepository.GetAttachmentById(id);
        }
        catch (Exception exception)
        {
            LogError("GetFromMemory", exception);
            throw;
        }
    }

    private void LogError(string methodName, Exception exception)
    {
        _logger.LogCritical("'{0}' method '{1}' exception. \n{2}.", GetType().Name, methodName, exception);
    }

}
