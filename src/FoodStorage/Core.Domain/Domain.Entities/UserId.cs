using FoodStorage.Domain.Entities.Common;

namespace FoodStorage.Domain.Entities;

/// <summary>
/// Идентификатор пользователя
/// </summary>
public record UserId
{
    private readonly Guid _value;

    private UserId(Guid value)
    {
        _value = value;
    }

    public Guid ToGuid() => _value;

    public static UserId FromGuid(Guid value)
    {
        // У пользователя не делаем проверку на пустой гуид,
        // т.к. пустой гуид будет "системным пользователем" 
        return new UserId(value);
    }
}
