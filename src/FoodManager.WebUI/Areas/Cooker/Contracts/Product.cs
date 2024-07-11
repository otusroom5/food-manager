namespace FoodManager.WebUI.Areas.Cooker.Contracts;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UnitType { get; set; }
    public double MinAmountPerDay { get; set; }
    public int BestBeforeDate { get; set; }
}

public class ProductShort
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}
