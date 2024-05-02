using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUsersService
{
    (UserDto User, string Password) CreateUser(UserDto user);
    IEnumerable<UserDto> GetAll();
    void UpdateUser(UserDto user);
    void DisableUser(Guid id);
    void ChangePassword(string loginName, string password);
    UserDto VerifyAndGetUserIfSuccess(string loginName, string password);
}
