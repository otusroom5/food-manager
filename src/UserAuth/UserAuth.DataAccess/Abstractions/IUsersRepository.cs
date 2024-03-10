using UserAuth.DataAccess.Entities;

namespace UserAuth.DataAccess.Abstractions
{
    public interface IUsersRepository
    {
        Guid Create(User user);
        void Update(User user);
        void Delete(Guid id);
        IEnumerable<User> GetAll();
        User? FindUserByName(string userName);
    }
}
