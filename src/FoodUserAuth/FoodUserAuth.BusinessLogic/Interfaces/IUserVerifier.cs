namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUserVerifier
{
    Task<bool> VerifyAsync(string loginName, string password);
}