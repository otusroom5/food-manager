using FoodUserAuth.BusinessLogic.Dto;

namespace FoodUserAuth.BusinessLogic.Interfaces;

public interface IUserVerifier
{
    Task<UserDto> VerifyAndReturnUserIfSuccessAsync(string loginName, string password);
}