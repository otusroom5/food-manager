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

namespace FoodUserAuth.BusinessLogic.Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UsersService> _logger;

    private readonly User _predefinedUser;

    public UsersService(IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IPasswordGenerator passwordGenerator,
        IHttpContextAccessor httpContextAccessor,
        ILogger<UsersService> logger
        )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
        _passwordGenerator = passwordGenerator;
        _predefinedUser = CreatePredefinedUser(unitOfWork.GetUsersRepository(), passwordHasher);
    }

    private User CreatePredefinedUser(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            LoginName = "predefined",
            Email = "predefined@foodmanager.com",
            Role = DataAccess.Types.UserRole.Administrator,
            IsDisabled = usersRepository.GetAllAsync().Result.Any(),
            Password = passwordHasher.ComputeHash("predefined")
        };
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

        if (!_passwordHasher.VerifyHash(password, foundUser.Password))
        {
            throw new NotValidPasswordException();
        }

        return foundUser.ToDto();
    }


    private async Task<User> InternalFindUserByLoginNameAsync(string loginName)
    {   
        User foundUser = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(loginName);

        if (foundUser == null)
        {
            if (!IsPredefinedLoginName(loginName))
            {
                throw new UserNotFoundException(); 
            }

            foundUser = _predefinedUser;
        }

        if (foundUser.IsDisabled)
        {
            throw new UserDisabledException();
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
            throw new UserAlreadyExistException();
        }

        string newPassword = _passwordGenerator.GeneratePassword(user.LoginName);
        
        var entity = user.ToEntity();
        
        entity.Id = Guid.NewGuid();
        entity.IsDisabled = false;
        entity.Password = _passwordHasher.ComputeHash(newPassword);

        _unitOfWork.GetUsersRepository().Create(entity);
        
        await _unitOfWork.SaveChangesAsync();

        var result = entity.ToDto();

        return (result, newPassword);
    }

    private bool IsPredefinedLoginName(string loginName)
    {
        return _predefinedUser?.LoginName.Equals(loginName, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

    /// <summary>
    /// This method disable exist user
    /// </summary>
    public async Task DisableUserAsync(Guid id)
    {
        User currentUser = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(GetCurrentLoginName());
       
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
    public async Task UpdateUserAsync(UserDto user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        User item = await InternalGetAsync(user.Id);

        item.FirstName = user.FirstName;
        item.LastName = user.LastName;
        item.Role = user.Role;
        item.Email = user.Email;

        await _unitOfWork.SaveChangesAsync();
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

    private string GetCurrentLoginName()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.Name))?.Value ?? string.Empty;
    }

    public async Task<UserDto> GetAsync(Guid id)
    {
        User foundUser = await _unitOfWork.GetUsersRepository().GetByIdAsync(id);
        return foundUser?.ToDto();
    }
}
