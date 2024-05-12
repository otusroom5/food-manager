using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ShopDeleteModel
{
    /// <summary>
    /// Shop Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}