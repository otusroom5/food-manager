namespace FoodUserAuth.DataAccess.Entities;

public sealed class ApiKey
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate {  get; set; }
}
