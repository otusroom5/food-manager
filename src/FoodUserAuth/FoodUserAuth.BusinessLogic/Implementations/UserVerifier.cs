using FoodUserAuth.BusinessLogic.Interfaces;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.DataAccess.Interfaces;
using FoodUserAuth.DataAccess.Types;

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

    public async Task<bool> VerifyAsync(string loginName, string password)
    {
        User user = await FindByLoginNameAsync(loginName);

        if (!_passwordHasher.VerifyHash(password, user.Password))
        {
            return false;
        }

        return true;
    }

    private async Task<User> FindByLoginNameAsync(string loginName)
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

    private bool IsPredefinedLoginName(string loginName)
    {
        return _predefinedUser?.LoginName.Equals(loginName, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }
}
