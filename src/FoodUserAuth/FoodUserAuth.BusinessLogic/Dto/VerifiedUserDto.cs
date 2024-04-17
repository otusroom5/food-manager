using FoodUserAuth.BusinessLogic.Types;

namespace FoodUserAuth.BusinessLogic.Dto;

public sealed class VerifiedUserDto : IEquatable<VerifiedUserDto>
{
    public string UserName { get; set; }
    public UserRole Role { get; set; }

    public bool Equals(VerifiedUserDto other)
    {
        return other is not null &&
               UserName.Equals(other.UserName) &&
               Role == other.Role;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as VerifiedUserDto);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserName, Role);
    }
}
