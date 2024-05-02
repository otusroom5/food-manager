using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Abstractions;

public interface IUsersRepository
{
    void Create(User user);
    void Update(User user);
    void Delete(User user);
    User GetById(Guid id);
    IEnumerable<User> GetAll();
    User FindByLoginName(string loginName);
}
