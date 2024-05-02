namespace FoodUserAuth.DataAccess.Types;

[Flags]
public enum UserRole : byte
{
    Administrator = 0b_0000_0001,
    Cooker = 0b_0000_0010,
    Manager = 0b_0000_0100
}
