namespace FoodManager.WebUI.Options;

public class AuthenticationOptions
{
    public static string Authentication = "Authentication";
    public string ClaimsIssuer { get; set; }
    public string CookieName { get; set; }
    public int ExpireTimeMin { get; set; }
}
