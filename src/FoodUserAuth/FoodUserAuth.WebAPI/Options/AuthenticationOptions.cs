namespace FoodUserAuth.WebApi.Options;

public class AuthenticationOptions
{
    public static string Authentication = "Authentication";
    public string TokenIssuer { get; set; }
    public string SecurityKey { get; set; }
    public string Audience { get; set; }
    public int TokenExpirySec { get; set; }
}
