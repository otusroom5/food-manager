using FoodUserNotifier.Core.Entities.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;

namespace FoodUserNotifier.Infrastructure.Repositories.Repositories;

public class TelegramSessionsRepository : ITelegramSessionsRepository
{
    private readonly DatabaseContext _context;
    public TelegramSessionsRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public void Create(TelegramSession session)
    {
        ;
    }

    public Task<TelegramSession> GetSessionByRecepientIdAsync(Guid recepientId)
    {
        throw new NotImplementedException();
    }
}
