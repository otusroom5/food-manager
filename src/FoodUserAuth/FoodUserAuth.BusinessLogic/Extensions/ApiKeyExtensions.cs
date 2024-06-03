using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Extensions;

public static class ApiKeyExtensions
{
    public static ApiKeyDto ToDto(this ApiKey apiKey) 
    { 
        return new ApiKeyDto()
        {
            Id = apiKey.Id,
            ExpiryDate = apiKey.ExpiryDate,
            Key = apiKey.Token
        };
    }
}
