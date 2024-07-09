namespace FoodPlanner.DataAccess.Models;

public sealed class ProductEntity
{
    public Guid Id { get; set; }
    public ProductDetailsEntity Product { get; set; } = new ProductDetailsEntity();
}

public sealed class ProductDetailsEntity
{    
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Amount { get; set; }
    public DateTime СreatingDate { get; set; }    
    public DateTime? ExpiryDate { get; set; }
}
