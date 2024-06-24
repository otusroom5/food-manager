namespace FoodUserAuth.WebApi.Utils;

public class ApiKeyData
{
    public Guid KeyId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ValidTo { get; set; }
}
