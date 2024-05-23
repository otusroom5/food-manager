using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ShopGetModel
{
    /// <summary>
    /// Shop Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}