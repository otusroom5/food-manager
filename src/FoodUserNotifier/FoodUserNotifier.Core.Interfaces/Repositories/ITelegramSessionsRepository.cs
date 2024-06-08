using FoodUserNotifier.Core.Entities.Entities;

namespace FoodUserNotifier.Core.Interfaces.Repositories;

public interface ITelegramSessionsRepository
{
    void Create(TelegramSession session);
    Task<TelegramSession> GetSessionByRecepientIdAsync(Guid recepientId);
}
