using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductIndexModel
{
    public ProductModel[] Products { get; set; }

    public ProductModel Product { get; set; }
}

public class ProductModel
{
    [HttpTableColumnKey]
    public string Id { get; set; }

    [HttpTableColumn("Product")]
    public string Name { get; set; }

    [HttpTableColumn("Unit")]
    public string UnitType { get; set; }

    [HttpTableColumn("Min Amount Per Day")]
    public string MinAmountPerDay { get; set; }

    [HttpTableColumn("Before Date")]
    public string BestBeforeDate { get; set; }
}