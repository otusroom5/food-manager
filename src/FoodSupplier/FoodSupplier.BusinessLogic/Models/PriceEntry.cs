namespace FoodSupplier.BusinessLogic.Models;

public class PriceEntry
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShopId { get; set; }
    public DateTimeOffset Date { get; set; }
    public decimal Price { get; set; }

    public PriceEntry(Guid productId, Guid shopId, DateTimeOffset date, decimal price)
    {
        ProductId = productId;
        ShopId = shopId;
        Date = date;
        Price = price;
    }
}