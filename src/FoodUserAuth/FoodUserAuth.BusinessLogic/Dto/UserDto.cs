using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public sealed class UserDto : IEquatable<UserDto>, ICloneable
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserState State { get; set; }

        public object Clone()
        {
           return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserDto);
        }

        public bool Equals(UserDto other)
        {
            return other is not null &&
                   Id.Equals(other.Id) &&
                   LoginName == other.LoginName &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Email == other.Email &&
                   State == other.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, LoginName, FirstName, LastName, Email, State);
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
