using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public class UserDto : IEquatable<UserDto?>
    {
        public Guid Id { get; }
        public string UserName { get; set; } = null!;
        public string FullName {  get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserState State { get; }

        public UserDto(Guid id, UserState state)
        {
            Id = id;
            State = state;
        }

        public UserDto(UserState state) : this(Guid.Empty, state) {}

        public override bool Equals(object? obj)
        {
            return Equals(obj as UserDto);
        }

        public bool Equals(UserDto? other)
        {
            return other is not null &&
                   Id.Equals(other.Id) &&
                   UserName == other.UserName &&
                   FullName == other.FullName &&
                   Email == other.Email &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserName, FullName, Email, State);
        }

        public static bool operator ==(UserDto? left, UserDto? right)
        {
            return EqualityComparer<UserDto>.Default.Equals(left, right);
        }

        public static bool operator !=(UserDto? left, UserDto? right)
        {
            return !(left == right);
        }
    }
}
