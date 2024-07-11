namespace FoodPlanner.BusinessLogic.Models;

public class ExpireProduct
{
    public List<ExpireProductDetails> ProductItems { get; set; } = new List<ExpireProductDetails>();
    public DateTime OccuredOn { get; set; }
}

public class ExpireProductDetails
{       
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime CreatingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }       
}