namespace FoodPlanner.BusinessLogic.Models;

public class ProductAlmostOver
{
    public string ProductName { get; set; } = string.Empty;
    public int MinAmountPerDay { get; set; } 
    public int Amount { get; set; } 
    public string Unit { get; set; } = string.Empty;
    public DateTime OccuredOn { get; set; }
}