using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Cooker.Models;

public class ProductHistoryIndexModel
{
    public ProductHistoryModel[] ProductHistoryItems { get; set; } = new ProductHistoryModel[0];

    public ProductModel Product { get; set; }

    public string[] Actions { get; set; }
}

public class ProductHistoryModel
{
    [HttpTableColumnKey]
    public string ProductId { get; set; }

    [HttpTableColumn("Product")]
    public string ProductName { get; set; }

    [HttpTableColumn("Action")]
    public string Action { get; set; }

    [HttpTableColumn("Count")]
    public string Count { get; set; }

    [HttpTableColumn("Unit")]
    public string Unit { get; set; }

    [HttpTableColumn("Created At")]
    public string CreatedAt { get; set; }
}
