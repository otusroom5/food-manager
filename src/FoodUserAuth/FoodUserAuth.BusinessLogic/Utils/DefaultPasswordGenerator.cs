using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;

namespace FoodUserAuth.BusinessLogic.Utils;

public class DefaultPasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
