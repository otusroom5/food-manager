using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUsersService
{
    Task<(UserDto User, string Password)> CreateUserAsync(UserDto user);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task UpdateUserAsync(UserDto user);
    Task DisableUserAsync(Guid id);
    Task ChangePasswordAsync(string loginName, string password);
    Task<UserDto> VerifyAndGetUserIfSuccessAsync(string loginName, string password);
}
