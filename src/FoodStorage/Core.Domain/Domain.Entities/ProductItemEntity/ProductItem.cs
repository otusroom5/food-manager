using FoodStorage.Domain.Entities.Common.DomainEvents;
using FoodStorage.Domain.Entities.Common.Exceptions;
using FoodStorage.Domain.Entities.ProductEntity;

namespace FoodStorage.Domain.Entities.ProductItemEntity;

/// <summary>
/// Единица продукта (продукт в холодильнике)
/// </summary>
public record ProductItem
{
    /// <summary>
    /// Идентификатор единицы продукта
    /// </summary>
    public ProductItemId Id { get; init; }

    /// <summary>
    /// Продукт
    /// </summary>
    public ProductId ProductId { get; init; }

    /// <summary>
    /// Количество продукта в холодильнике
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Дата изготовления
    /// </summary>
    public DateTime CreatingDate { get; init; }

    /// <summary>
    /// Дата окончания срока годности
    /// </summary>
    public DateTime ExpiryDate { get; init; }

    private readonly List<BaseDomainEvent> _domainEvents;
    /// <summary>
    /// Доменные события единицы продукта
    /// </summary>
    public List<BaseDomainEvent> DomainEvents => _domainEvents;

    private ProductItem(ProductId productId, int amount, UserId userId = null) 
    {
        ProductId = productId;
        Amount = amount;

        _domainEvents = new List<BaseDomainEvent>();

        // Если указан пользователь - значит создание из метода апи, т.е. надо отправить событие о записи.
        // В противном случае запись просто взята из бд, а значит она уже существует и не новая
        if (userId is not null)
        {
            RegisterEvent(new AddedProductItemDomainEvent(productId, amount, userId, DateTime.UtcNow));
        }
    }

    public static ProductItem CreateNew(ProductItemId id, ProductId productId, int amount, DateTime creatingDate, DateTime expiryDate, UserId userId = null)
    {
        if (amount <= 0)
        {
            throw new InvalidArgumentValueException("Количество единиц продукта должно быть положительным числом", nameof(Amount));
        }

        if (creatingDate.Date >= DateTime.UtcNow.Date)
        {
            throw new InvalidArgumentValueException("Дата изготовления не может быть из будущего", nameof(CreatingDate));
        }

        if (expiryDate.Date <= creatingDate.Date)
        {
            throw new InvalidArgumentValueException("Дата окончания срока годности не может быть раньше чем дата изготовления", nameof(ExpiryDate));
        }

        return new(productId, amount, userId)
        {
            Id = id,
            CreatingDate = creatingDate.Date,
            ExpiryDate = expiryDate.Date
        };
    }

    /// <summary>
    /// Забрать часть продукта из холодильника
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceAmount(int amount, UserId userId)
    {
        if (amount > Amount)
        {
            throw new DomainEntitiesException("Нельзя забрать продукта больше, чем есть");
        }

        Amount -= amount;

        RegisterEvent(new ReducedProductItemDomainEvent(this, amount, userId, DateTime.UtcNow));
    }

    /// <summary>
    /// Списать единицу продукта
    /// </summary>
    /// <param name="amount"></param>
    public void WriteOff(UserId userId)
    {
        RegisterEvent(new WritedOffProductItemDomainEvent(this, userId, DateTime.UtcNow));
    }

    /// <summary>
    /// Регистрация события
    /// </summary>
    /// <param name="domainEvent">доменное событие</param>
    private void RegisterEvent(BaseDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Очистка всех событий
    /// </summary>
    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
}
