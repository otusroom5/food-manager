namespace FoodSupplier.DataAccess.Entities;

public class PriceEntryEntity
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShopId { get; set; }
    public DateTimeOffset Date { get; set; }
    public decimal Price { get; set; }
}