using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Interfaces;
using System.Text;

namespace FoodUserAuth.BusinessLogic.Utils;

public class DefaultPasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword(string baseText)
    {
        return baseText + new Random().NextInt64().ToString();
    }
}
