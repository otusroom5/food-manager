using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductUpdateModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UnitType { get; set; }
    public string MinAmountPerDay { get; set; }
    public string BestBeforeDate { get; set; }
}
