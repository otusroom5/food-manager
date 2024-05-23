using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ShopCreateModel
{
    /// <summary>
    /// Shop name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Shop activity flag (default: false)
    /// </summary>
    [Required]
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Shop base Url
    /// </summary>
    [Required]
    public string BaseUrl { get; set; }
}