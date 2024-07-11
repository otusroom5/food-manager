namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductTakeOfModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string UnitType { get; set; }
    public double Count { get; set; }
    public string UnitId { get; set; }
}
