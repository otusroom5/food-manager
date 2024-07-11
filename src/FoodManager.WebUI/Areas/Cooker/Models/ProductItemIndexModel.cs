using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductItemIndexModel
{
    public ProductAction ProductAction { get; set; } = ProductAction.All; // по умолчанию смотреть все единицы продуктов

    public ProductItemModel[] ProductItems { get; set; } = new ProductItemModel[0];

    public ProductItemModel ProductItem { get; set; }

    public ProductModel Product { get; set; }

    public UnitModel[] Units { get; set; }
}

public class ProductItemModel
{
    [HttpTableColumnKey]
    public string Id { get; set; }

    [HttpTableColumnKey]
    public string ProductId { get; set; }

    [HttpTableColumn("Product")]
    public string ProductName { get; set; }

    [HttpTableColumn("Amount")]
    public string Amount { get; set; }

    [HttpTableColumn("Unit")]
    public string UnitId { get; set; }

    [HttpTableColumn("Creating Date")]
    public DateTime CreatingDate { get; set; }

    [HttpTableColumn("Expiry Date")]
    public DateTime ExpiryDate { get; set; }

    [HttpTableColumn("Is Expired")]
    public string IsExpired  => ExpiryDate > DateTime.UtcNow.Date ? "No" : "Yes"; 
}

public class UnitModel
{
    public string Id { get; set; }

    public string Name { get; set; }
}