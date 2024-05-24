namespace FoodManager.WebUI.Contracts;

public sealed class AuthenticationResponse : ResponseBase
{
    public AuthenticationData Data { get; set; }
}

public sealed class AuthenticationData
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}

