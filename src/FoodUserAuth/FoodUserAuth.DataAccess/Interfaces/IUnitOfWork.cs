namespace FoodUserAuth.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUsersRepository GetUsersRepository();
    Task SaveChangesAsync();
}
