using Domain.Entities.Exceptions;

namespace Domain.Entities;

/// <summary>
/// Идентификатор пользователя
/// </summary>
public record UserId
{
    private readonly Guid _value;

    private UserId(Guid userId)
    {
        if (userId != Guid.Empty)
        {
            throw new DomainEntitiesException($"Передано некорректное значение {nameof(UserId)}");
        }

        _value = userId;
    }

    public Guid ToGuid() => _value;
    public static UserId FromGuid(Guid value) => new UserId(value);
}
