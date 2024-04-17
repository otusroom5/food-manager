namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IPasswordHasher
{
    string ComputeHash(string password);
    bool VerifyHash(string password, string hashedPassword);
}
