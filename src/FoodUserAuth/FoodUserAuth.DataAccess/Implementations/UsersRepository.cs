using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.DataAccess.Data;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.DataAccess.Exceptions;

namespace FoodUserAuth.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDbContext _userDbContext;
        public UsersRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        public Guid Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _userDbContext.Users.Add(user);
            _userDbContext.SaveChanges();

            return user.Id;
        }

        /// <summary>
        /// Update user, throw exception if user is not found.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User foundUser = GetById(user.Id);

            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            foundUser.LoginName = user.LoginName;
            foundUser.Email = user.Email;
            foundUser.State = user.State;
            foundUser.Password = user.Password;

            _userDbContext.SaveChanges();
        }

        /// <summary>
        /// Delete user by id, if throw exception if user is not found.
        /// </summary>
        /// <param name="id"></param>
        ///<exception cref="NotFoundUserException"></exception>
        public void Delete(Guid id)
        {
            User foundUser = GetById(id);
            _userDbContext.Users.Remove(foundUser);
            _userDbContext.SaveChanges();
        }

        /// <summary>
        /// Return all list of users
        /// </summary>
        /// <returns>IEnumerable<User></returns>
        public IEnumerable<User> GetAll()
        {
            return _userDbContext.Users.ToList();
        }

        /// <summary>
        /// Find user by name, if user is not found then return null
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns>User</returns>
        public User FindUserByName(string loginName)
        {
            return (from users in _userDbContext.Users
                    where users.LoginName == loginName
                    select users).FirstOrDefault();
        }

        /// <summary>
        /// Get user by id, throw exception if user is not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        /// <exception cref="NotFoundUserException"></exception>
        public User GetById(Guid id)
        {
            User user = (from users in _userDbContext.Users
                         where users.Id == id
                         select users).FirstOrDefault();

            if (user is null)
            {
                throw new NotFoundUserException($"User {id} not found");
            }

            return user;
        }
    }
}
