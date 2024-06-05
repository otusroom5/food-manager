using FoodManager.WebUI.Areas.Administrator.Contracts;
using FoodManager.WebUI.Areas.Administrator.Models;

namespace FoodManager.WebUI.Extensions;

public static class ApiKeysExtensions
{
    public static ApiKeyModel ToModel(this ApiKey key)
    {
        return new ApiKeyModel()
        {
            Id = key.Id,
            Key = key.Key,
            ExpiryDate = key.ExpiryDate
        };
    }
}
