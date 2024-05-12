namespace FoodSupplier.BusinessLogic.Dto;

public class PriceEntryDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShopId { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
}