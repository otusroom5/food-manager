namespace FoodUserAuth.DataAccess.Types;

[Flags]
public enum UserContactType: byte
{
    None = 0b_0000_0000,
    Email = 0b_0000_0001,
    TelegramUserName = 0b_0000_0010
}
