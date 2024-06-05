using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;

namespace FoodUserAuth.WebApi.Extensions;

internal static class ApiKeyDtoExtensions
{
    public static ApiKeyModel ToModel(this ApiKeyDto model)
    {
        return new ApiKeyModel()
        {
            Id = model.Id,
            Key = model.Key,
            ExpiryDate = model.ExpiryDate
        };
    } 
}
