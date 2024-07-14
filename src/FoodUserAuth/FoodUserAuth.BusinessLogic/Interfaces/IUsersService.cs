using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUsersService
{
    Task<(UserDto User, string Password)> CreateAsync(UserDto user);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetAsync(Guid id);
    Task UpdateAsync(UserDto user);
    Task DisableAsync(Guid id);
    Task<UserDto> FindByLoginNameAsync(string loginName);
}
