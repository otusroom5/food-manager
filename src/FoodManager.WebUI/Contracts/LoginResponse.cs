namespace FoodManager.WebUI.Contracts;

public sealed class LoginResponse
{
    public string Token { get; set; }
    public string Role { get; set; }
    public string Message { get; set; }
}
