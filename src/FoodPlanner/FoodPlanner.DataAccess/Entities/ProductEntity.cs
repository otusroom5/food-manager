using System.ComponentModel;

namespace FoodPlanner.DataAccess.Models;

public sealed class ProductEntity
{
    public Guid Id { get; set; }  
    public Guid ProductId { get; set; }   
    public int Amount { get; set; }    
    public DateTime CreatingDate { get; set; }

    [DefaultValue(null)]    
    public DateTime? ExpiryDate { get; set; }
}
