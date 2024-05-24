using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUsersService
{
    Task<(UserDto User, string Password)> CreateUserAsync(UserDto user);
    Task<string> ResetPasswordAsync(Guid userId);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetAsync(Guid id);
    Task UpdateUserAsync(UserDto user);
    Task DisableUserAsync(Guid id);
    Task ChangePasswordAsync(string oldPassword, string newPassword);
    Task<UserDto> VerifyAndGetUserIfSuccessAsync(string loginName, string password);
}
