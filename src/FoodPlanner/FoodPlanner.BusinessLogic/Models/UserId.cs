using FoodPlanner.BusinessLogic.Exceptions;

namespace FoodPlanner.BusinessLogic.Models
{
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
}
