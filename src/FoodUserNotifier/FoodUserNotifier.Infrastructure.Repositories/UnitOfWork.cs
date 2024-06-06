using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;

namespace FoodUserNotifier.Infrastucture.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IReportsRepository _reportsRepository;
    
    public UnitOfWork(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public IReportsRepository GetReportsRepository()
    {
        if (_reportsRepository == null)
        {
            _reportsRepository = new ReportsRepository(_context);
        }

        return _reportsRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
