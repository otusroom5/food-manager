using FoodManager.WebUI.Contracts;

namespace FoodManager.WebUI.Areas.Administrator.Contracts.Responses;

public sealed class ApiKeyCreateResponse : ResponseBase
{
    public ApiKey Data { get; set; }
}
