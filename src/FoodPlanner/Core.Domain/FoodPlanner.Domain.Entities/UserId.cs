using FoodPlanner.Domain.Entities.Common.Exceptions;

namespace FoodPlanner.Domain.Entities;

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
        value.ValidateOrThrow(nameof(UserId));
        return new UserId(value);
    }
}