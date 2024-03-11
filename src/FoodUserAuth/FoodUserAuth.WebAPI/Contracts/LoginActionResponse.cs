namespace FoodUserAuth.WebApi.Contracts
{
    public class LoginActionResponse
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
