using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.BusinessLogic.Extensions;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;

namespace FoodUserAuth.BusinessLogic.Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordGenerator _passwordGenerator;

    public UsersService(IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IPasswordGenerator passwordGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _passwordGenerator = passwordGenerator;
    }

    /// <summary>
    /// This method change user password
    /// </summary>
    public async Task ChangePasswordAsync(string loginName, string password)
    {
        User user = await InternalFindUserByLoginNameAsync(loginName);

        user.Password = _passwordHasher.ComputeHash(password);

        _unitOfWork.GetUsersRepository().Update(user);
        
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// This method verify user
    /// </summary>
    public async Task<UserDto> VerifyAndGetUserIfSuccessAsync(string loginName, string password) 
    {
        User foundUser = await InternalFindUserByLoginNameAsync(loginName);

        if (_passwordHasher.VerifyHash(password, foundUser.Password))
        {
            throw new NotValidPasswordException("Не верный пароль");
        }

        return foundUser.ToDto();
    }


    private async Task<User> InternalFindUserByLoginNameAsync(string loginName)
    {
        User foundUser = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(loginName);

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
    public async Task<(UserDto User, string Password)> CreateUserAsync(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(user.LoginName) != null)
        {
            throw new UserAlreadyExistException("Пользователь с таким именем уже есть");
        }

        var entity = user.ToEntity();
        entity.Id = Guid.NewGuid();
        entity.IsDisabled = false;

        await _unitOfWork.GetUsersRepository().CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var result = entity.ToDto();

        return (result, _passwordGenerator.GeneratePassword(result));
    }

    /// <summary>
    /// This method disable exist user
    /// </summary>
    public async Task DisableUserAsync(Guid id)
    {
        User item = await InternalGetAsync(id);
        item.IsDisabled = true;
        _unitOfWork.GetUsersRepository().Update(item);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// This method return all available users
    /// </summary>
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var items = await _unitOfWork.GetUsersRepository().GetAllAsync();

        return items.Select(f => f.ToDto());
    }

    /// <summary>
    /// This method update detaisl of user
    /// </summary>
    public async Task UpdateUserAsync(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        User item = await InternalGetAsync(user.Id);

        item.FirstName = user.FirstName;
        item.LastName = user.LastName;
        item.Email = user.Email;
        item.LoginName = user.LoginName;

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<User> InternalGetAsync(Guid id)
    {
        User item = await _unitOfWork.GetUsersRepository().GetByIdAsync(id);

        if (item == null)
        {
            throw new UserNotFoundException($"Пользователь не найден");
        }

        return item;
    }
}
