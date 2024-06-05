using FoodPlanner.DataAccess.Interfaces;

namespace FoodPlanner.DataAccess.Implementations;

public class UnitOfWork: IUnitOfWork
{
    private readonly HttpClient _httpClient;
    private IStorageRepository _storageRepository;    

    public UnitOfWork(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IStorageRepository GetStorageRepository()
    {
        _storageRepository ??= new StorageRepository(_httpClient);

        return _storageRepository;
    }   
}
