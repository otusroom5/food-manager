using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Abstractions
{
    public interface IUsersRepository
    {
        Guid Create(User user);
        void Update(User user);
        void Delete(Guid id);
        User GetById(Guid id);
        IEnumerable<User> GetAll();
        User FindUserByName(string loginName);
    }
}
