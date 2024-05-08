namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IPasswordGenerator
{
    string GeneratePassword(string baseText);
}
