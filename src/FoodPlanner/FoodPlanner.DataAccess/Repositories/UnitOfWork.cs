using FoodPlanner.DataAccess.Interfaces;

namespace FoodPlanner.DataAccess.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly InMemoryDbContext _databaseContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private IStorageRepository _storageRepository;
    private IReportRepository _reportRepository;

    public UnitOfWork(IHttpClientFactory httpClientFactory,
        InMemoryDbContext databaseContext)
    {
        _httpClientFactory = httpClientFactory;
        _databaseContext = databaseContext;
    }

    public IStorageRepository GetStorageRepository()
    {
        _storageRepository ??= new StorageRepository(_httpClientFactory);

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
