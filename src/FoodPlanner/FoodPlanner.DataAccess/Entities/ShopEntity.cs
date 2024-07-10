namespace FoodPlanner.DataAccess.Entities;

public class ShopEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;   
    public bool IsActive { get; set; }
}