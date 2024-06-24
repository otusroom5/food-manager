using FoodPlanner.DataAccess.Interfaces;

namespace FoodPlanner.DataAccess.Implementations;

public class UnitOfWork: IUnitOfWork
{
    private readonly InMemoryDbContext _databaseContext;
    private readonly HttpClient _httpClient;
    private IStorageRepository _storageRepository;    
    private IReportRepository _reportRepository;    

    public UnitOfWork(HttpClient httpClient,
        InMemoryDbContext databaseContext)
    {
        _httpClient = httpClient;
        _databaseContext = databaseContext;
    }

    public IStorageRepository GetStorageRepository()
    {
        _storageRepository ??= new StorageRepository(_httpClient);

        return _storageRepository;
    }

    public IReportRepository GetReportRepository()
    {
        if (_reportRepository == null)
        {
            _reportRepository = new ReportRepository(_databaseContext);
        }

        return _reportRepository;
    }
}
