using MediatR;

namespace FoodStorage.Domain.Entities.Common.DomainEvents;

/// <summary>
/// Базовое доменное событие
/// </summary>
public abstract class BaseDomainEvent : INotification
{
    /// <summary>
    /// Произошло кем
    /// </summary>
    public UserId OccuredBy { get; }

    /// <summary>
    /// Произошло когда
    /// </summary>
    public DateTime OccuredOn { get; }

    protected BaseDomainEvent(UserId occuredBy, DateTime occuredOn)
    {
        OccuredBy = occuredBy;
        OccuredOn = occuredOn;
    }
}
