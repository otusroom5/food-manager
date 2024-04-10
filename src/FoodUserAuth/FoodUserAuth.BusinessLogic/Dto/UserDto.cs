using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public class UserDto : IEquatable<UserDto>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName {  get; set; }
        public string Email { get; set; }
        public UserState State { get; set; }


        public override bool Equals(object obj)
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

        public static bool operator ==(UserDto left, UserDto right)
        {
            return EqualityComparer<UserDto>.Default.Equals(left, right);
        }

        public static bool operator !=(UserDto left, UserDto right)
        {
            return !(left == right);
        }
    }
}
