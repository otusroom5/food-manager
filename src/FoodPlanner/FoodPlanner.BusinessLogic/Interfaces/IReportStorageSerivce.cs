using FoodPlanner.DataAccess.Entities;

namespace FoodPlanner.BusinessLogic.Interfaces;

public interface IReportStorageSerivce
{   
    public void SaveInMemory(ReportEntity reportEntity);
    
    public byte[]? GetFromMemory(Guid id);
}
