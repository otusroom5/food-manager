namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductCreateModel
{
    public string Name { get; set; }
    public string UnitType { get; set; }
    public string MinAmountPerDay { get; set; }
    public string BestBeforeDate { get; set; }
}
