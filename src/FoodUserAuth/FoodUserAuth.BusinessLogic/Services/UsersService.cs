using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.BusinessLogic.Interfaces;

namespace FoodUserAuth.BusinessLogic.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordGenerator _passwordGenerator;

    public UsersService(IUsersRepository usersRepository, 
        IPasswordHasher passwordHasher,
        IPasswordGenerator passwordGenerator) 
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _passwordGenerator = passwordGenerator;
    }

    /// <summary>
    /// This method change user password
    /// </summary>
    public void ChangePassword(Guid id, string password)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method create user
    /// </summary>
    public (UserDto User, string Password) CreateUser(UserDto user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method disable exist user
    /// </summary>
    public void DisableUser(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method return all available users
    /// </summary>
    public IEnumerable<UserDto> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method update detaisl of user
    /// </summary>
    public void UpdateUser(UserDto user)
    {
        throw new NotImplementedException();
    }
}
