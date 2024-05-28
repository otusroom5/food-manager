namespace FoodSupplier.BusinessLogic.Models;

public class Shop
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BaseUrl { get; set; }
    public bool IsActive { get; set; }
}