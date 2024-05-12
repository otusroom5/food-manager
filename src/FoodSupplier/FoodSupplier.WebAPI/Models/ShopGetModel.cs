using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ShopGetModel
{
    /// <summary>
    /// Shop Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Shop activity flag (default: true)
    /// </summary>
    public bool IsActive { get; set; } = true;
}