using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class SingleProduceModel
{
    /// <summary>
    /// Shop Id
    /// </summary>
    [Required]
    public Guid ShopId { get; set; }

    /// <summary>
    /// Product Id
    /// </summary>
    [Required]
    public Guid ProductId { get; set; }
}