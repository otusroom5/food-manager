namespace FoodPlanner.DataAccess.Interfaces;

public interface IUnitOfWork
{
    public IStorageRepository GetStorageRepository();
    public ISupplierRepository GetSupplierRepository();
    public IReportRepository GetReportRepository();        
}
