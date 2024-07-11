namespace FoodPlanner.DataAccess.Models;

public sealed class ProductEntity
{
    public Guid Id { get; set; }
    public ProductDetailsEntity Product { get; set; } = new ProductDetailsEntity();
    public int Amount { get; set; }    
    public DateTime CreatingDate { get; set; }    
    public DateTime? ExpiryDate { get; set; }    
}

public sealed class ProductDetailsEntity
{    
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;    
}
