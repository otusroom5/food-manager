namespace FoodManager.WebUI.Areas.Administrator.Contracts;

public sealed class ApiKey
{
    public string Id { get; set; }
    public string Key { get; set; }
    public string ExpiryDate { get; set; }
}
