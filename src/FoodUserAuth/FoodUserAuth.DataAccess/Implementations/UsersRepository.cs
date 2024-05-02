using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.DataAccess.DataContext;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.DataAccess.Repositories;

public class UsersRepository : IUsersRepository, IDisposable
{
    private readonly DatabaseContext _userDbContext;
    private bool _disposedValue;

    public UsersRepository(DatabaseContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Guid</returns>

    public void Create(User user)
    {
        _userDbContext.Users.Add(user);
    }

    /// <summary>
    /// Update user, throw exception if user is not found.
    /// </summary>
    /// <param name="user"></param>
    public void Update(User user)
    {
        _userDbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    /// <summary>
    /// Delete user by id, if throw exception if user is not found.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(User user) 
    {
        _userDbContext.Users.Remove(user);
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
    public User FindByLoginName(string loginName)
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
    public User GetById(Guid id)
    {
        return (from users in _userDbContext.Users
                     where users.Id == id
                     select users).FirstOrDefault();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _userDbContext.Dispose();
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {     
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
