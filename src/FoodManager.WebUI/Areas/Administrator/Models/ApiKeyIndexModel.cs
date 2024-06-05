using FoodManager.WebUI.Utils;

namespace FoodManager.WebUI.Areas.Administrator.Models;

public sealed class ApiKeyIndexModel
{
    public ApiKeyModel[] ApiKeys { get; set; }

    public ApiKeyModel ApiKey { get; set; }
}

public sealed class ApiKeyModel
{
    [HttpTableColumnKey()]
    public string Id { get; set; }

    [HttpTableColumn("Key", "overflow: auto;", "width: 600px;")]
    public string Key { get; set; }

    [HttpTableColumn("Expired", "", "width: 150px;")]
    public string ExpiryDate { get; set; }
}
