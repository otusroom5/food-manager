using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.DataAccess.Data;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDbContext _userDbContext;
        public UsersRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public Guid Create(User user)
        {
            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();

            return user.Id;
        }
        public void Update(User updatedUser)
        {
            if (updatedUser != null)
            {
                var user = _userDbContext.Users.FirstOrDefault(f => f.Id == updatedUser.Id);
                if (user != null)
                {
                    user.Password = updatedUser.Password;
                    _userDbContext.SaveChanges();
                }
            }
        }

        public void Delete(Guid id)
        {
            var user = _userDbContext.Users.FirstOrDefault(f => f.Id == id);
            if (user != null)
            {
                _userDbContext.Users.Remove(user);
                _userDbContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _userDbContext.Users.ToList();
        }

        public User? FindUserByName(string userName)
        {
            throw new NotImplementedException();
        }      
    }
}
