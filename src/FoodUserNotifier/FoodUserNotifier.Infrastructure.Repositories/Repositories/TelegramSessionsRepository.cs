using FoodUserNotifier.Core.Entities.Entities;
using FoodUserNotifier.Core.Interfaces.Repositories;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

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
        _context.Add(session);
    }

    public async Task<TelegramSession> FindSessionByChatIdAsync(long chatId)
    {
        return await _context.TelegramSessions.FirstOrDefaultAsync(f => f.ChatId == chatId);
    }

    public async Task<TelegramSession> GetSessionByRecepientIdAsync(Guid recepientId)
    {
        return await _context.TelegramSessions.FirstOrDefaultAsync(f => f.RecepientId == recepientId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
