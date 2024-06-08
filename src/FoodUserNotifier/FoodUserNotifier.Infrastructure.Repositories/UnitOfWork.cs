using FoodUserNotifier.Core.Interfaces;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using FoodUserNotifier.Infrastructure.Repositories.Repositories;

namespace FoodUserNotifier.Infrastucture.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IReportsRepository _reportsRepository;
    private ITelegramSessionsRepository _telegramSessionsRepository;

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

    public ITelegramSessionsRepository GetTelegramSessionsRepository()
    {
        if (_telegramSessionsRepository == null)
        {
            _telegramSessionsRepository = new TelegramSessionsRepository(_context);
        }

        return _telegramSessionsRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
