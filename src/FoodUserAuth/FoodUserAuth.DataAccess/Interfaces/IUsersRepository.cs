using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Abstractions
{
    public interface IUsersRepository
    {
        void Create(User user);
        void Update(User user);
        void Delete(Guid id);
        User GetById(Guid id);
        IEnumerable<User> GetAll();
        User FindUserByLoginName(string loginName);
        void Save();
    }
}
