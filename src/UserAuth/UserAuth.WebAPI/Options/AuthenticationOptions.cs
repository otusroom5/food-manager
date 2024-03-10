namespace UserAuth.WebApi.Options
{
    public class AuthenticationOptions
    {
        public string TokenIssuer { get; set; } = "FoodStorage.UserAuth";
        public string SecurityKey { get; set; } = "1ys5persecre1_secret5ecretsec6etkey!423";
        public string Audience { get; set; } = "services";
        public int TokenExpirySec { get; set; } = 1440;
    }
}
