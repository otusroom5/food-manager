using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Abstractions;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.BusinessLogic.Extensions;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;

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
    public void ChangePassword(string loginName, string password)
    {
        User user = InternalFindUserByLoginName(loginName);

        user.Password = _passwordHasher.ComputeHash(password);

        _usersRepository.Update(user);
    }

    /// <summary>
    /// This method verify user
    /// </summary>
    public UserDto VerifyAndGetUserIfSuccess(string loginName, string password) 
    {
        User foundUser = InternalFindUserByLoginName(loginName);

        if (_passwordHasher.VerifyHash(password, foundUser.Password))
        {
            throw new NotValidPasswordException("Не верный пароль");
        }

        return foundUser.ToDto();
    }


    private User InternalFindUserByLoginName(string loginName)
    {
        User foundUser = _usersRepository.FindByLoginName(loginName);

        if (foundUser == null)
        {
            throw new UserNotFoundException("Пользователь не найден");
        }

        if (foundUser.IsDisabled)
        {
            throw new UserDisabledException("Пользователь не доступен");
        }

        return foundUser;
    }


    /// <summary>
    /// This method create user
    /// </summary>
    public (UserDto User, string Password) CreateUser(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (_usersRepository.FindByLoginName(user.LoginName) != null)
        {
            throw new UserAlreadyExistException("Пользователь с таким именем уже есть");
        }

        var entity = user.ToEntity();
        entity.Id = Guid.NewGuid();
        entity.IsDisabled = false;

        _usersRepository.Create(entity);
        var result = entity.ToDto();

        return (result, _passwordGenerator.GeneratePassword(result));
    }

    /// <summary>
    /// This method disable exist user
    /// </summary>
    public void DisableUser(Guid id)
    {
        User item = InternalGet(id);
        item.IsDisabled = true;
        _usersRepository.Update(item);
    }

    /// <summary>
    /// This method return all available users
    /// </summary>
    public IEnumerable<UserDto> GetAll()
    {
        return _usersRepository
                .GetAll()
                .Select(f => f.ToDto());
    }

    /// <summary>
    /// This method update detaisl of user
    /// </summary>
    public void UpdateUser(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        User item = InternalGet(user.Id);

        item.FirstName = user.FirstName;
        item.LastName = user.LastName;
        item.Email = user.Email;
        item.LoginName = user.LoginName;
    }

    private User InternalGet(Guid id)
    {
        User item = _usersRepository.GetById(id);

        if (item == null)
        {
            throw new UserNotFoundException($"Пользователь не найден");
        }

        return item;
    }
}
