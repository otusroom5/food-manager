namespace FoodManager.WebUI.Areas.Cooker.Contracts;

public class ProductHistory
{
    public ProductShort Product { get; set; }

    public string State { get; set; }

    public double Count { get; set; }

    public string Unit { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; init; }
}
