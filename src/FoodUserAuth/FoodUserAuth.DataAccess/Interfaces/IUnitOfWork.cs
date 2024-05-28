namespace FoodUserAuth.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUsersRepository GetUsersRepository();
    IApiKeyRepository GetApiKeyRepository();
    Task SaveChangesAsync();
}
