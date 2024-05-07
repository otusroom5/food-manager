using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Interfaces;

public interface IUsersRepository
{
    Task CreateAsync(User user);
    void Update(User user);
    void Delete(User user);
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> FindByLoginNameAsync(string loginName);
}
