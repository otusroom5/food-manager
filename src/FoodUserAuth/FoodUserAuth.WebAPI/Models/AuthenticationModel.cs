namespace FoodUserAuth.WebApi.Models;
public class AuthenticationModel
{
    public string UserId {  get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}
