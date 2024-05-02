namespace FoodUserAuth.WebApi.Contracts;
public class LoginActionResponse
{
    public string Token { get; set; }
    public string Role { get; set; }
    public string Message { get; set; }
}
