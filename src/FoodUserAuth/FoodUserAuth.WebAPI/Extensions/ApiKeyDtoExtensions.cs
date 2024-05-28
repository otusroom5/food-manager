using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.WebApi.Models;

namespace FoodUserAuth.WebApi.Extensions;

public static class ApiKeyDtoExtensions
{
    public static ApiKeyModel ToModel(this ApiKeyDto model)
    {
        return new ApiKeyModel()
        {
            Id = model.Id,
            Token = model.Token,
            ExpiryDate = model.ExpiryDate
        };
    } 
}
