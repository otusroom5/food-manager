using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.DataAccess.Entities;

namespace FoodUserAuth.BusinessLogic.Extensions;

public static class ApiKeyDtoExtensions
{
    public static ApiKey ToModel(this ApiKeyDto apiKeyDto)
    {
        return new ApiKey()
        {
            Id = apiKeyDto.Id,
            ExpiryDate = apiKeyDto.ExpiryDate,
            Token = apiKeyDto.Key
        };
    }
}
