namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductCreateModel
{
    public string Name { get; set; }
    public string UnitType { get; set; }
    public double MinAmountPerDay { get; set; }
    public int BestBeforeDate { get; set; }
}
