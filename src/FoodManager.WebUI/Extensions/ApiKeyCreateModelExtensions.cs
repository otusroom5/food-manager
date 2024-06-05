using FoodManager.WebUI.Areas.Administrator.Contracts.Requests;
using FoodManager.WebUI.Areas.Administrator.Models;

namespace FoodManager.WebUI.Extensions;

internal static class ApiKeyCreateModelExtensions
{
    public static ApiKeyCreateRequest ToRequest(this ApiKeyCreateModel model)
    {
        return new ApiKeyCreateRequest()
        {
            ExpiryDate = model.ExpiryDate
        };
    }
}
