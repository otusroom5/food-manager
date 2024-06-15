namespace FoodUserAuth.WebApi.Contracts.Requests;

public sealed class ApiKeyRenewTokenRequest
{
    public string OldToken { get; set; }
}
