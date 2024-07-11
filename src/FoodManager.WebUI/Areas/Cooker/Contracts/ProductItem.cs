namespace FoodManager.WebUI.Areas.Cooker.Contracts;

public class ProductItem
{
    public Guid Id { get; set; }

    public ProductShort Product { get; set; }

    public double Amount { get; set; }

    public string Unit { get; set; }

    public DateTime CreatingDate { get; set; }

    public DateTime ExpiryDate { get; init; }
}
