using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.BusinessLogic.Extensions;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.BusinessLogic.Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly ILogger<UsersService> _logger;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public UsersService(IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IPasswordGenerator passwordGenerator,
        ICurrentUserAccessor currentUserAccessor,
        ILogger<UsersService> logger
        )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _passwordGenerator = passwordGenerator;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<UserDto> FindByLoginNameAsync(string loginName)
    {
        User user = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(loginName);
        
        return user?.ToDto();
    }


    /// <summary>
    /// This method create user
    /// </summary>
    public async Task<(UserDto User, string Password)> CreateAsync(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(user.LoginName) != null)
        {
            throw new UserAlreadyExistException();
        }

        string newPassword = _passwordGenerator.GeneratePassword(user.LoginName);

        var entity = user.ToEntity();

        entity.Id = Guid.NewGuid();
        entity.IsDisabled = false;
        entity.Password = _passwordHasher.ComputeHash(newPassword);

        _unitOfWork.GetUsersRepository().Create(entity);

        if (user.Email != null)
        {
            _unitOfWork.GetUserContactsRepository().Create(new UserContact()
            {
                Id = Guid.NewGuid(),
                UserId = entity.Id,
                ContactType = UserContactType.Email,
                Contact = user.Email
            });
        }

        if (user.Telegram != null)
        {
            _unitOfWork.GetUserContactsRepository().Create(new UserContact()
            {
                Id = Guid.NewGuid(),
                UserId = entity.Id,
                ContactType = UserContactType.Telegram,
                Contact = user.Telegram
            });
        }

        await _unitOfWork.SaveChangesAsync();

        var result = entity.ToDto();

        return (result, newPassword);
    }

    /// <summary>
    /// This method disable exist user
    /// </summary>
    public async Task DisableAsync(Guid id)
    {
        UserDto currentUser = await _currentUserAccessor.GetCurrentUserAsync();

        if (id.Equals(currentUser?.Id))
        {
            throw new NotValidUserException("You can not possibility you disabled this user");
        }

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
    public async Task UpdateAsync(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        User item = await InternalGetAsync(user.Id);

        item.FirstName = user.FirstName;
        item.LastName = user.LastName;
        item.Role = user.Role;

        await UpdateContact(item.Id, UserContactType.Email, user.Email);
        await UpdateContact(item.Id, UserContactType.Telegram, user.Telegram);

        await _unitOfWork.SaveChangesAsync();
    }

    async Task UpdateContact(Guid userId, UserContactType contactType, string contactAddress)
    {
        var contact = await _unitOfWork
            .GetUserContactsRepository()
            .GetByUserIdAndContactTypeAsync(userId, contactType, false);

        if (contact != null)
        {
            contact.Contact = contactAddress;
        }
        else
        {
            _unitOfWork.GetUserContactsRepository().Create(new UserContact()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ContactType = contactType,
                Contact = contactAddress
            });
        }
    }

    private async Task<User> InternalGetAsync(Guid id)
    {
        User item = await _unitOfWork.GetUsersRepository().GetByIdAsync(id);

        if (item == null)
        {
            throw new UserNotFoundException();
        }

        return item;
    }

    public async Task<UserDto> GetAsync(Guid id)
    {
        User foundUser = await _unitOfWork.GetUsersRepository().GetByIdAsync(id);
        return foundUser?.ToDto();
    }
}
