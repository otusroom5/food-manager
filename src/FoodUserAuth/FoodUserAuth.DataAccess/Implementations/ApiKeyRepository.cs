using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodUserAuth.DataAccess.Implementations;

internal class ApiKeyRepository : IApiKeyRepository, IDisposable
{
    private readonly DatabaseContext _dbContext;
    private bool disposedValue;

    public ApiKeyRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

  
    public void Create(ApiKey key)
    {
        _dbContext.Add(key);
    }

    public void Delete(ApiKey key)
    {
        _dbContext.Remove(key);
    }

    public async Task<IEnumerable<ApiKey>> GetAllAsync()
    {
        return await _dbContext.ApiKeys.ToListAsync();
    }

    public void Update(ApiKey key)
    {
        _dbContext.Entry(key).State = EntityState.Modified;
    }

    public async Task<ApiKey> GetByIdOrDefaultAsync(Guid id)
    {
        return await _dbContext.ApiKeys.FirstOrDefaultAsync(f => f.Id.Equals(id));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }



}
