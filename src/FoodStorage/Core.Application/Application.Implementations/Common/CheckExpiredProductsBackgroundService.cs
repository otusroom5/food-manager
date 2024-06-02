using FoodStorage.Application.Services;
using FoodStorage.Domain.Entities;
using FoodStorage.Domain.Entities.ProductEntity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodStorage.Application.Implementations.Common;

public sealed class CheckExpiredProductsBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider _services;
    private readonly CheckExpiredProductsConfiguration _configuration;
    private Timer _timer = null;

    public CheckExpiredProductsBackgroundService(IServiceProvider services, CheckExpiredProductsConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Сервис запущен");

        // Если не указано целевое время, то устанавливаем по умолчанию 8 утра
        if (!TimeSpan.TryParse(_configuration.CheckExpiredProductsTimeOfDay, out TimeSpan timeSpanTarget))
        {
            timeSpanTarget = TimeSpan.FromHours(8);
        }

        // Текущее время дня
        TimeSpan timeSpanNow = DateTime.Now.TimeOfDay;

        // Определение через какое время наступит время события
        TimeSpan delayTime = timeSpanTarget >= timeSpanNow
            ? timeSpanTarget - timeSpanNow
            : TimeSpan.FromDays(1) - (timeSpanNow - timeSpanTarget);

        _timer = new Timer(o => _ = ExecuteAsync(), null, delayTime, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Сервис остановлен");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async Task ExecuteAsync()
    {
        UserId userId = UserId.FromGuid(Guid.Empty);

        using IServiceScope scope = _services.CreateScope();
        IProductItemService productItemService = scope.ServiceProvider.GetRequiredService<IProductItemService>();

        // Получаем все просроченные продукты и списываем их
        var expiredProductItems = productItemService.GetExpireProductItems();
        productItemService.WriteOff(expiredProductItems.Select(epi => epi.Id), userId);

        // Получаем все продукты, у которых срок годности заканачивается через определенное время (указанные n дней)
        var expireProductItems = productItemService.GetExpireProductItems(_configuration.TimeBeforeExpireInDaysForReport);
        var products = expireProductItems.Select(epi => epi.ProductId).Distinct().ToList();

        throw new NotImplementedException("Will be rabbit message"); //TODO
    }

    public void Dispose() => _timer?.Dispose();
}
