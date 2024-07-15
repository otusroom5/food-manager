using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Types;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Extensions;

namespace FoodUserAuth.BusinessLogic.Services;

public class UserVerifier : IUserVerifier
{
    private const string PredefineLoginName = "predefined";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly User _predefinedUser;

    public UserVerifier(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _predefinedUser = CreatePredefinedUser(unitOfWork.GetUsersRepository(), passwordHasher);
    }

    private User CreatePredefinedUser(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            LoginName = PredefineLoginName,
            Role = UserRole.Administrator,
            IsDisabled = usersRepository.GetAllAsync().Result.Any(),
            Password = passwordHasher.ComputeHash(PredefineLoginName)
        };
    }

    public async Task<UserDto> VerifyAndReturnUserIfSuccessAsync(string loginName, string password)
    {
        User user = await FindByLoginNameAsync(loginName);

        if (!_passwordHasher.VerifyHash(password, user.Password))
        {
            return null;
        }

        return user.ToDto();
    }

    private async Task<User> FindByLoginNameAsync(string loginName)
    {
        User user = await _unitOfWork.GetUsersRepository().FindByLoginNameAsync(loginName);

        if (user == null)
        {
            if (!IsPredefinedLoginName(loginName))
            {
                throw new UserNotFoundException();
            }

            user = _predefinedUser;
        }

        if (user.IsDisabled)
        {
            throw new UserDisabledException();
        }

        return user;
    }

    private bool IsPredefinedLoginName(string loginName)
    {
        return _predefinedUser?.LoginName.Equals(loginName, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
}
