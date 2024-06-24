namespace FoodPlanner.DataAccess.Interfaces;

public interface IUnitOfWork
{
    public IStorageRepository GetStorageRepository();        
    public IReportRepository GetReportRepository();        
}
